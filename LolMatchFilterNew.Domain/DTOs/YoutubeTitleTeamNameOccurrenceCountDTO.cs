using Domain.DTOs.TeamnameDTOs;


namespace Domain.DTOs.YoutubeTitleTeamNameOccurrenceCountDTOs
{
    public class YoutubeTitleTeamNameOccurrenceCountDTO
    {
        public TeamNameDTO? TeamNameDto { get; set; }

        public string YoutubeTitle { get; set; } = string.Empty; 

        public int LongNameCount { get; set; } = 0;
        public List<string>? LongNameMatches {  get; set; }  
        public int MediumNameCount { get; set; } = 0;
        public List<string>? MediumNameMatches { get; set; }
        public int ShortNameCount { get; set; } = 0;
        public List<string>? ShortNameMatches { get; set; }
        public List<string>? MatchingInputs { get; set; }
    }
}
