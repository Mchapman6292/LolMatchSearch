namespace Domain.DTOs.YoutubeDataWithTeamsDTOs
{
    // Used to format data from Immport_YoutubeData so MatchComparisonController can extract team names from title. Is not linked to YoutubeDataWithTeamsDTO db table. 
    public class YoutubeDataWithTeamsDTO
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
