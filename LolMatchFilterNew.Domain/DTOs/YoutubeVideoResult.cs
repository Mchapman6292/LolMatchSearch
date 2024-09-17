using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Document.NET;

namespace LolMatchFilterNew.Domain.DTOs.YoutubeVideoResult
{
    public class YoutubeVideoResult
    {
        public string VideoId { get; set; }
        public string Title { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string ThumbnailUrl { get; set; }
        public string YoutubeResultHyperlink { get; set; }


        public YoutubeVideoResult(string videoId, string url)
        {
            VideoId = videoId;
            YoutubeResultHyperlink = $"https://www.youtube.com/watch?v={videoId}";
        }
    }
}
