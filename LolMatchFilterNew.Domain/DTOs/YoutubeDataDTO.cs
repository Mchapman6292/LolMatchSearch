namespace Domain.DTOs.YoutubeDataDTOs
{
    public class YoutubeDataDTO
    {
        public string YoutubeVideoId { get; set; }
        public string VideoTitle { get; set; }
        public string? PlaylistId { get; set; }
        public string? PlaylistTitle { get; set; }
        public DateTime? PublishedAtUtc { get; set; }
        public string? YoutubeResultHyperlink { get; set; }
        public string? ThumbnailUrl { get; set; }
    }
}
