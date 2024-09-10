using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Entities.YoutubeVideoData
{
    public class YoutubeVideoData
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Duration { get; set; }
    }
}
