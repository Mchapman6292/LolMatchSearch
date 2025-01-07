using Domain.Interfaces.ApplicationInterfaces.ITeamNameValidators;
using Domain.Interfaces.ApplicationInterfaces.MatchFailureReasons;
using LolMatchFilterNew.Domain.DTOs.MatchComparisonResultDTOs;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchComparisonResultBuilders;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;

namespace Application.MatchPairingService.MatchComparisonResultService.MatchComparisonResultBuilders
{
    public class MatchComparisonResultBuilder : IMatchComparisonResultBuilder
    {
        private readonly MatchComparisonResultDTO _result = new();
        private readonly ITeamNameValidator _teamValidator;

        private bool _teamsSet;
        private bool _youtubeInfoSet;
        private bool _matchDateSet;

        public List<string> Errors { get; private set; } = new();
        public List<string> Warnings { get; private set; } = new();
        public List<string> InfoMessages { get; private set; } = new();



        public MatchComparisonResultBuilder(ITeamNameValidator teamValidator)
        {
            _teamValidator = teamValidator;
   
        }


        private void AddError(string message)
        {
            Errors.Add(message);
            SetNoMatch(); 
        }

        private void AddWarning(string message)
        {
            Warnings.Add(message);
        }

        private void AddInfo(string message)
        {
            InfoMessages.Add(message);
        }


        private void SetNoMatch()
        {
            _result.DoesMatch = false;
        }

        private void SetMatch()
        {
            _result.DoesMatch = true;
        }



        // Returning an interface instead of void/concrete class allows for method chaining. 
        public IMatchComparisonResultBuilder WithMatchIdentification(string gameId)
        {
            if (string.IsNullOrWhiteSpace(gameId))
                throw new ArgumentException("Game ID cannot be empty", nameof(gameId));
            _result.ScoreboardGames = gameId;
            return this;
        }

        public IMatchComparisonResultBuilder WithMatchDate(DateTime? matchDate)
        {
            _result.MatchDate = matchDate;
            _matchDateSet = true;
            return this;
        }

        public IMatchComparisonResultBuilder WithLeaguepediaTeams(string team1, string team2)
        {
            if (string.IsNullOrWhiteSpace(team1))
  
            if (string.IsNullOrWhiteSpace(team2))
                throw new ArgumentException("Team2 cannot be empty", nameof(team2));

            _result.LeaguepediaTeam1 = team1;
            _result.LeaguepediaTeam2 = team2;
            _teamsSet = true;
            return this;
        }

        public IMatchComparisonResultBuilder WithYoutubeInfo(string videoId, string title, DateTime? publishDate)
        {
            if (string.IsNullOrWhiteSpace(videoId))
                throw new ArgumentException("Video ID cannot be empty", nameof(videoId));
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("VideoTitle cannot be empty", nameof(title));

            _result.YoutubeVideoId = videoId;
            _result.YoutubeTitle = title;
            _result.YoutubePublishDate = publishDate;
            _youtubeInfoSet = true;
            return this;
        }

        public IMatchComparisonResultBuilder WithYoutubeTeams(string youtubeVideoId, string extractedTeam1, string extractedTeam2)
        {
       
            int? matchCount = _teamValidator.GetCountOfValidTeams(_result.YoutubeTeam1, _result.YoutubeTeam2);





            return this;
        }

        public IMatchComparisonResultBuilder WithExtractedTeamInfo(string extractedTeamInfo)
        {
            _result.ExtractedTeamInfo = extractedTeamInfo;
            return this;
        }



        public MatchComparisonResultDTO Build()
        {
            if (!_teamsSet)
                throw new InvalidOperationException("Teams must be set before building the result");

            if (!_youtubeInfoSet)
                throw new InvalidOperationException("YouTube information must be set before building the result");

            if (!_matchDateSet)
                throw new InvalidOperationException("Match date must be set before building the result");

            return _result;
        }





    }






}

