namespace Domain.DTOs.Processed_YoutubeDataDTOs
{
    // Used to format data from Immport_YoutubeData so MatchComparisonController can extract team names from title. Is not linked to Processed_YoutubeDataDTO db table. 
    public class Processed_YoutubeDataDTO
    {
        public string YoutubeVideoId { get; set; }
        public string VideoTitle { get; set; }
        public string? PlaylistId { get; set; }
        public string? PlaylistTitle { get; set; }
        public DateTime? PublishedAtUtc { get; set; }
        public string? YoutubeResultHyperlink { get; set; }
        public string? ThumbnailUrl { get; set; }

        public string? Team1 { get; set; }
        public string? Team2 { get; set; }

    }
}
