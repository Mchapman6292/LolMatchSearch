using Domain.Interfaces.ApplicationInterfaces.IMatchDTOServices.IImport_TeamNameServices;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamNameFinders;
using Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;


// Edge cases Title: Vitality vs Splyce Highlights | EU LCS Week 9 Day 2 Spring 2016 S6 | VIT vs SPY
/* INF] Title: Vitality vs Splyce Highlights | EU LCS Week 9 Day 2 Spring 2016 S6 | VIT vs SPY
[07:37:16 INF]     Team ID: Splyce | Found matches: [SPY, Splyce, Splyce, splyce, spy]
[07:37:16 INF]     Team ID: Szef+6 | Found matches: [S6, s6]
LONGNAME =Team Vitality
[07:37:16 INF] ----------------------------------------*/




/*
----------------------------------------
Title: Giants vs Roccat Game 1 Highlights, EU LCS W9D2 Summer 2016 Season 6, GIA vs ROC G1
youtube_video_id: IX--eOUe9Uw
----------------------------------------
            ALL MATCHES:
            Team ID: Giants (Spanish Team)_71
            Found matches: [Medium: Giants, Long: Giants, Input: giants]
            Team ID: VIRUS (Greek Team)_8
            Found matches: [Short: VS]

            MOST FREQUENT MATCHES:
            Team ID: Giants (Spanish Team)_71
            Found matches: [Medium: Giants, Long: Giants, Input: giants]
            Team ID: VIRUS (Greek Team)_8
            Found matches: [Short: VS]

    - MATCHING VS
    - ShortNames not matching?

*/




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


        public YoutubeTitleTeamNameFinder(IAppLogger appLogger, IObjectLogger objectLogger, IImport_TeamNameService import_TeamNameService)
        {
            _appLogger = appLogger;
            _objectLogger = objectLogger;
            _importTeamNameService = import_TeamNameService;
        }


        public bool CheckNameMatch(string nameToCheck, string normalizedTitle)
        {
            if (string.IsNullOrEmpty(nameToCheck)) throw new ArgumentException($" {nameof(CheckNameMatch)} cannot be empty");

            string normalizedName = nameToCheck.ToLower();
            if (IsExactWordOccurrence(normalizedTitle, normalizedName))
            {
                return true;
            }
            return false;
        }



        //Checks if a given teamName exists as a standalone term in a youtubeTitle string by verifying it's surrounded by either spaces, punctuation, or string boundaries. Prevents partial matches like finding "SK" within "SKT".
        // This returns the first occurrence only but since we are going through every property in the list of TeamNameDTOs if we miss a match with one variation, we'll catch it with another.


        public bool IsExactWordOccurrence(string youtubeTitle, string teamName)
        {

            if (string.IsNullOrEmpty(teamName)) return false;

            int index = youtubeTitle.IndexOf(teamName);
            if (index == -1) return false;

            bool validStart = index == 0 || !char.IsLetterOrDigit(youtubeTitle[index - 1]);
            bool validEnd = index + teamName.Length == youtubeTitle.Length || !char.IsLetterOrDigit(youtubeTitle[index + teamName.Length]);

            return validStart && validEnd;
        }







    }

}
