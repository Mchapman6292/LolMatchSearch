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


        private readonly HashSet<string> _commonExclusions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "vs",
            "na", 
            "s1", "s2", "s3", "s4", "s5", "s6", "s7", "s8", "s9", "s10", "s11", // Seasons
            "w1", "w2", "w3", "w4", "w5", "w6", "w7", "w8", "w9", "w10", // Weeks
            "d1", "d2", "d3", "d4", "d5", "d6", "d7"  // Days
        };







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
                if (extractedWord.Equals(teamNameDto.ShortName))
                {
                    allAbbreviationMatches.Add(teamNameDto.ShortName);
                    youtubeDto.IncrementCount("Short", 1);
                }
            }
            if (!string.IsNullOrEmpty(teamNameDto.MediumName))
            {
                if (extractedWord.Equals(teamNameDto.MediumName))
                {
                    allAbbreviationMatches.Add(teamNameDto.MediumName);
                    youtubeDto.IncrementCount("Medium", 1);
                }
            }

            if (!string.IsNullOrEmpty(teamNameDto.LongName))
            {
                if (extractedWord.Equals(teamNameDto.LongName))
                {
                    allAbbreviationMatches.Add(teamNameDto.LongName);
                    youtubeDto.IncrementCount("Long", 1);
                }
            }

            if (teamNameDto.FormattedInputs != null && teamNameDto.FormattedInputs.Count > 0)
            {
                foreach (var input in teamNameDto.FormattedInputs)
                {
                    if (extractedWord.Equals(input))
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


















        public void ProcessYoutubeTitle(YoutubeTitleTeamNameOccurrenceCountDTO occurrenceDTO)
        {
            string normalizedTitle = occurrenceDTO.YoutubeTitle.ToLower();

            foreach (var teamNameDto in _importTeamNameService.ReturnImport_TeamNameAllNames())
            {
                var matchesForTeam = new List<string>();

                CheckNameMatch(teamNameDto.ShortName, "Short", normalizedTitle, occurrenceDTO, matchesForTeam);
                CheckNameMatch(teamNameDto.MediumName, "Medium", normalizedTitle, occurrenceDTO, matchesForTeam);
                CheckNameMatch(teamNameDto.LongName, "Long", normalizedTitle, occurrenceDTO, matchesForTeam);

                if (teamNameDto.FormattedInputs != null)
                {
                    foreach (var input in teamNameDto.FormattedInputs)
                    {
                        CheckNameMatch(input, "Inputs", normalizedTitle, occurrenceDTO, matchesForTeam);
                    }
                }

                if (matchesForTeam.Any())
                {
                    occurrenceDTO.UpdateMatchingTeamNameIds(teamNameDto.TeamNameId, matchesForTeam);
                }
            }
        }

        private void CheckNameMatch(string? nameToCheck, string nameType, string normalizedTitle,YoutubeTitleTeamNameOccurrenceCountDTO occurrenceDTO, List<string> matchesForTeam)
        {
            if (string.IsNullOrEmpty(nameToCheck)) return;

            string normalizedName = nameToCheck.ToLower();
            if (IsWholeWord(normalizedTitle, normalizedName))
            {
                matchesForTeam.Add(nameToCheck);
                occurrenceDTO.IncrementCount(nameType, 1);

                if (nameType == "Inputs" && occurrenceDTO.MatchingInputs != null)
                {
                    occurrenceDTO.MatchingInputs.Add(nameToCheck);
                }
            }
        }




        private bool IsWholeWord(string text, string word)
        {

            if (string.IsNullOrEmpty(word)) return false;

            int index = text.IndexOf(word);
            if (index == -1) return false;

            bool validStart = index == 0 || !char.IsLetterOrDigit(text[index - 1]);
            bool validEnd = index + word.Length == text.Length || !char.IsLetterOrDigit(text[index + word.Length]);

            return validStart && validEnd;
        }




    }

}
