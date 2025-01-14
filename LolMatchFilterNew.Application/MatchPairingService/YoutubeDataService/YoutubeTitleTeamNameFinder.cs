using Domain.DTOs.TeamnameDTOs;
using Domain.DTOs.YoutubeTitleTeamNameOccurrenceCountDTOs;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamNameFinders;
using Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using Domain.Interfaces.ApplicationInterfaces.IMatchDTOServices.IImport_TeamNameServices;
using Microsoft.Extensions.ObjectPool;
using OfficeOpenXml.Packaging.Ionic.Zip;



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









        public void UpdateDTOWithTeamMatchesFromTitle(YoutubeTitleTeamNameOccurrenceCountDTO youtubeTitleMatchDTO)
        {
            string[] extractedWords = ExtractAllWordsFromYoutubeTitle(youtubeTitleMatchDTO.YoutubeTitle);
            foreach (var teamNameDto in _importTeamNameService.ReturnImport_TeamNameAllNames())
            {
                var allTeamMatches = new List<string>();

                foreach (string word in extractedWords)
                {
                    var wordMatches = GetTeamNameMatchesInExtractedWord(teamNameDto, youtubeTitleMatchDTO, word);
                    allTeamMatches.AddRange(wordMatches);
                }

                if (allTeamMatches.Any())
                {
                    youtubeTitleMatchDTO.UpdateMatchingTeamNameIds(teamNameDto.TeamNameId, allTeamMatches);
                }
            }
        }


        private List<string> GetTeamNameMatchesInExtractedWord(TeamNameDTO teamNameDto, YoutubeTitleTeamNameOccurrenceCountDTO youtubeDto, string extractedWord)
        {
            List<string> allAbbreviationMatches = new List<string>();

            if (!string.IsNullOrEmpty(teamNameDto.ShortName))
            {
                if (extractedWord.Contains(teamNameDto.ShortName))
                {
                    allAbbreviationMatches.Add(teamNameDto.ShortName);
                    youtubeDto.IncrementCount("Short", 1);
                }
            }
            if (!string.IsNullOrEmpty(teamNameDto.MediumName))
            {
                if (extractedWord.Contains(teamNameDto.MediumName))
                {
                    allAbbreviationMatches.Add(teamNameDto.MediumName);
                    youtubeDto.IncrementCount("Medium", 1);
                }
            }

            if (!string.IsNullOrEmpty(teamNameDto.LongName))
            {
                if (extractedWord.Contains(teamNameDto.LongName))
                {
                    allAbbreviationMatches.Add(teamNameDto.LongName);
                    youtubeDto.IncrementCount("Long", 1);
                }
            }

            if (teamNameDto.FormattedInputs != null && teamNameDto.FormattedInputs.Count > 0)
            {
                foreach (var input in teamNameDto.FormattedInputs)
                {
                    if (extractedWord.Contains(input))
                    {
                        allAbbreviationMatches.Add(input);
                        youtubeDto.IncrementCount("Inputs", 1);
                    }
                }
            }
            if (allAbbreviationMatches.Count > 0)
            {
                _appLogger.Info($"Matches added for {youtubeDto.YoutubeTitle}, team: {teamNameDto.LongName}, total matches: {allAbbreviationMatches.Count}.");
                return allAbbreviationMatches;
            }
            return allAbbreviationMatches;
        }

    }
}