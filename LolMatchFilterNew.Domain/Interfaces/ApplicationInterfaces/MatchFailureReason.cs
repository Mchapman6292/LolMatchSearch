
public enum MatchMessageType
{
    Failure,      // Complete failures that prevent matching
    Warning,    // Potential issues that need attention but don't prevent matching
    Info        // Informational messages about the matching process
}

namespace Domain.Interfaces.ApplicationInterfaces.MatchFailureReasons
{
    public static class MatchFailureReason
    {
        // Import_YoutubeData title related failures.
        public const string NO_VS_KEYWORD = "Title does not contain 'vs' or 'versus' keyword";
        public const string NO_TEAM_NAMES_FOUND = "Unable to extract both team names from Youtube title pattern";
        public const string ONE_TEAM_NOT_FOUND = "Unable to extract one team name from Youtube title pattern";
        public const string MISSING_SEPARATOR = "Video title does not contain required separator '|'";
        public const string MULTIPLE_SEPARATORS = "Video title contains multiple '|' separators";
        public const string EMPTY_TEAM_SECTION = "Team section after separator is empty";

        public const string NO_MATCHING_TEAM = "No matching team found in known teams list";
        public const string AMBIGUOUS_TEAM_MATCH = "Multiple possible team matches found";


        //Import_ScoreboardGames data quality
        public const string INCOMPLETE_TEAM_DATA = "Team record missing required fields";
        public const string MALFORMED_TEAM_INPUTS = "Team inputs not properly formatted in database";
        public const string INCOMPLETE_MATCH_DATA = "Match record missing essential fields";






    }

   

}
