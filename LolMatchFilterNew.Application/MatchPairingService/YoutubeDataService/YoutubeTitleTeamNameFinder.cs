using Application.MatchPairingService.YoutubeDataService.YoutubeTitleTeamNameMatchResults;
using Domain.Interfaces.ApplicationInterfaces.IMatchDTOServices.IImport_TeamNameServices;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamNameFinders;
using Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using System.Text.RegularExpressions;


// Edge cases Title: Vitality vs Splyce Highlights | EU LCS Week 9 Day 2 Spring 2016 S6 | VIT vs SPY
/* INF] Title: Vitality vs Splyce Highlights | EU LCS Week 9 Day 2 Spring 2016 S6 | VIT vs SPY
[07:37:16 INF]     Team ID: Splyce | Found matches: [SPY, Splyce, Splyce, splyce, spy]
[07:37:16 INF]     Team ID: Szef+6 | Found matches: [S6, s6]
LONGNAME =Team Vitality
[07:37:16 INF] ----------------------------------------*/


// Exclude date format to reduce 
// Define Tournaments/Teams to narrow list of potential matches?
// Get a Teams all known opponents from SG and use that as the search parameters?



namespace Application.MatchPairingService.YoutubeDataService.YoutubeTitleTeamNameFinders
{
    public class YoutubeTitleTeamNameFinder : IYoutubeTitleTeamNameFinder
    {

        private readonly IAppLogger _appLogger;
        private readonly IObjectLogger _objectLogger;
        private readonly IImport_TeamNameService _importTeamNameService;


        private static readonly Regex G2_Game2_FalsePositive = new Regex(@"[Gg]2$", RegexOptions.Compiled);



        public YoutubeTitleTeamNameFinder(IAppLogger appLogger, IObjectLogger objectLogger, IImport_TeamNameService import_TeamNameService)
        {
            _appLogger = appLogger;
            _objectLogger = objectLogger;
            _importTeamNameService = import_TeamNameService;
        }



        // EXCLUSION LOGIC MUST CHANGE
        public void ProcessYoutubeTitle(YoutubeTitleTeamNameMatchResult occurrenceDTO)
        {
            string normalizedTitle = occurrenceDTO.YoutubeTitle.ToLower();

            // Check if title ends with G2
            bool endsWithG2 = G2_Game2_FalsePositive.IsMatch(normalizedTitle);

            // If it does, create a temporary version without G2 for matching
            string titleForMatching = endsWithG2
                ? normalizedTitle.Substring(0, normalizedTitle.Length - 2).Trim()
                : normalizedTitle;



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
                    occurrenceDTO.UpdateMatchingTeamNameIds(teamNameDto.LongName, matchesForTeam);
                }
            }
        }




        private void CheckNameMatch(string? nameToCheck, string nameType, string normalizedTitle,YoutubeTitleTeamNameMatchResult occurrenceDTO, List<string> matchesForTeam)
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


        /*
         Checks if a given word exists as a standalone term in a text string by verifying it's surrounded by either spaces, punctuation, or string boundaries. 
         Prevents partial matches like finding "SK" within "SKT".
        */

public bool IsWholeWord(string text, string word)
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
