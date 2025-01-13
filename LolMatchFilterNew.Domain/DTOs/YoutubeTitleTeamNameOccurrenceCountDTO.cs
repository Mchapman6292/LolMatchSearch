using Domain.DTOs.TeamnameDTOs;


namespace Domain.DTOs.YoutubeTitleTeamNameOccurrenceCountDTOs
{
    public class YoutubeTitleTeamNameOccurrenceCountDTO
    {
        public TeamNameDTO TeamNameDto { get; set; }
        public int LongNameMatch { get; set; } = 0;
        public int MediumNameMatch { get; set; } = 0;
        public int ShortNameMatch { get; set; } = 0;
        public List<string> MatchingInputs { get; set; }
    }
}
