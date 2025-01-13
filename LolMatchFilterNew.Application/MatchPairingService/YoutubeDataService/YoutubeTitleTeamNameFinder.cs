using Domain.DTOs.TeamnameDTOs;
using Domain.DTOs.YoutubeTitleTeamNameOccurrenceCountDTOs;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamNameFinders;
using Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using Domain.Interfaces.ApplicationInterfaces.IMatchDTOServices.IImport_TeamNameServices;



namespace Application.MatchPairingService.YoutubeDataService.YoutubeTitleTeamNameFinders
{
    public class YoutubeTitleTeamNameFinder : IYoutubeTitleTeamNameFinder
    {
        private readonly char[] Separators = new char[] { ' ', '-', '_', '|' };

        private readonly IAppLogger _appLogger;
        private readonly IObjectLogger _objectLogger;
        private readonly IImport_TeamNameService _importTeamNameService;




        public YoutubeTitleTeamNameFinder(IAppLogger appLogger, IObjectLogger objectLogger, IImport_TeamNameService import_TeamNameService)
        {
            _appLogger = appLogger;
            _objectLogger = objectLogger;
            _importTeamNameService = import_TeamNameService;
        }


        public string[] ExtractAllWordsFromYoutubeTitle(string youtubeTitle)
        {
            string formattedTitled = youtubeTitle.ToLower();
            return formattedTitled.Split(Separators, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }



        public void IncrementAllCountsForMatchesInTitle(string youtubeTitle, List<YoutubeTitleTeamNameOccurrenceCountDTO> youtubeTitleTeamMatchCounts)
        {
            IncrementShortNameMatchesInTitle(youtubeTitle, youtubeTitleTeamMatchCounts);
            IncrementMediumNameMatches(youtubeTitle, youtubeTitleTeamMatchCounts);
            IncrementLongNameMatches(youtubeTitle, youtubeTitleTeamMatchCounts);
            AddFormattedInputMatches(youtubeTitle, youtubeTitleTeamMatchCounts);
        }


        // Can try changing equals to contains. 

        public void IncrementShortNameMatchesInTitle(string youtubeTitle, List<YoutubeTitleTeamNameOccurrenceCountDTO> listOfTitleMatches)
        {
            string[] words = ExtractAllWordsFromYoutubeTitle(youtubeTitle);
            foreach (var titleMatch in listOfTitleMatches)
            {
                foreach (var word in words)
                {
                    if (!string.IsNullOrEmpty(titleMatch.TeamNameDto.Short) &&
                        word.Equals(titleMatch.TeamNameDto.Short, StringComparison.OrdinalIgnoreCase) &&
                        !titleMatch.ShortNameMatches.Contains(word))
                    {
                        titleMatch.ShortNameCount++;
                        titleMatch.ShortNameMatches.Add(word);
                    }
                }
            }
        }

        public void IncrementMediumNameMatches(string youtubeTitle, List<YoutubeTitleTeamNameOccurrenceCountDTO> listOfTitleMatches)
        {
            string[] words = ExtractAllWordsFromYoutubeTitle(youtubeTitle);
            foreach (var titleMatch in listOfTitleMatches)
            {
                foreach (var word in words)
                {
                    if (!string.IsNullOrEmpty(titleMatch.TeamNameDto.Medium) &&
                        word.Equals(titleMatch.TeamNameDto.Medium, StringComparison.OrdinalIgnoreCase) &&
                        !titleMatch.MediumNameMatches.Contains(word))
                    {
                        titleMatch.MediumNameCount++;
                        titleMatch.MediumNameMatches.Add(word);
                    }
                }
            }
        }

        public void IncrementLongNameMatches(string youtubeTitle, List<YoutubeTitleTeamNameOccurrenceCountDTO> listOfTitleMatches)
        {
            string[] words = ExtractAllWordsFromYoutubeTitle(youtubeTitle);
            foreach (var titleMatch in listOfTitleMatches)
            {
                foreach (var word in words)
                {
                    if (!string.IsNullOrEmpty(titleMatch.TeamNameDto.LongName) &&
                        word.Equals(titleMatch.TeamNameDto.LongName, StringComparison.OrdinalIgnoreCase) &&
                        !titleMatch.LongNameMatches.Contains(word))
                    {
                        titleMatch.LongNameCount++;
                        titleMatch.LongNameMatches.Add(word);
                    }
                }
            }
        }

        public void AddFormattedInputMatches(string youtubeTitle, List<YoutubeTitleTeamNameOccurrenceCountDTO> listOfTitleMatches)
        {
            string[] words = ExtractAllWordsFromYoutubeTitle(youtubeTitle);
            foreach (var titleMatch in listOfTitleMatches)
            {
                if (titleMatch.TeamNameDto.FormattedInputs != null)
                {
                    foreach (var word in words)
                    {
                        foreach (var input in titleMatch.TeamNameDto.FormattedInputs)
                        {
                            if (!string.IsNullOrEmpty(input) &&
                                word.Equals(input, StringComparison.OrdinalIgnoreCase) &&
                                !titleMatch.MatchingInputs.Contains(input))
                            {
                                titleMatch.MatchingInputs.Add(input);
                            }
                        }
                    }
                }
            }
        }
    }
}
