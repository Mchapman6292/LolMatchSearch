using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.DTOs.MatchComparisonResultDTOs;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchComparisonResultBuilders;

namespace LolMatchFilterNew.Application.MatchPairingService
{
    public class MatchComparisonResultBuilder : IMatchComparisonResultBuilder
    {
        private readonly MatchComparisonResultDTO _result = new();
        private bool _teamsSet;
        private bool _youtubeInfoSet;
        private bool _matchDateSet;


        public IMatchComparisonResultBuilder WithMatchIdentification(string gameId)
        {
            if (string.IsNullOrWhiteSpace(gameId))
                throw new ArgumentException("Game ID cannot be empty", nameof(gameId));
            _result.LeaguepediaGameId = gameId;
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
                throw new ArgumentException("Team1 cannot be empty", nameof(team1));
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

        public IMatchComparisonResultBuilder WithYoutubeTeams(string team1, string team2)
        {
            _result.YoutubeTeam1 = team1;
            _result.YoutubeTeam2 = team2;
            return this;
        }

        public IMatchComparisonResultBuilder WithExtractedTeamInfo(string extractedTeamInfo)
        {
            _result.ExtractedTeamInfo = extractedTeamInfo;
            return this;
        }

        public IMatchComparisonResultBuilder SetMatchResult(bool doesMatch, string mismatchReason = null)
        {
            _result.DoesMatch = doesMatch;
            _result.MismatchReason = mismatchReason;
            return this;
        }

        public MatchComparisonResultDTO Build()
        {
            if (!_teamsSet)
                throw new InvalidOperationException("Processed_LeagueTeams must be set before building the result");

            if (!_youtubeInfoSet)
                throw new InvalidOperationException("YouTube information must be set before building the result");

            if (!_matchDateSet)
                throw new InvalidOperationException("Match date must be set before building the result");

            return _result;
        }
    }


}

