using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Document.NET;

namespace LolMatchFilterNew.Domain.DTOs.YoutubeVideoDTOs
{
    // Needed? Possible older version of Import_YoutubeDataEntity
    public class Import_YoutubeVideoDTO
    {
        public string VideoId { get; set; }
        public string Title { get; set; }
        public string? Teams { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string ThumbnailUrl { get; set; }
        public string YoutubeResultHyperlink { get; set; }


        public Import_YoutubeVideoDTO(string videoId, string url)
        {
            VideoId = videoId;
            YoutubeResultHyperlink = $"https://www.youtube.com/watch?v={videoId}";
        }

        public void SetYoutubeTeams(string youtubeTeams)
        {
            Teams = youtubeTeams;
        }
    }
}
