﻿
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities

{
    // DB table = YoutubeVideoResults
    public class Import_YoutubeDataEntity
    {
        [Key]

        [Comment("Can begin with uppercase letters, numbers, lowercase letters, - and _ , appending single quotation to handle this.")]
        // Can begin with uppercase letters, numbers, lowercase letters, - and _ 
        public string YoutubeVideoId { get; set; }

        [Required]
        [MaxLength(255)]
        public string VideoTitle { get; set; }

        public string PlaylistId { get; set; }

        public string PlaylistTitle { get; set; }


        public DateTime PublishedAt_utc { get; set; }

        [MaxLength(2083)]  // Max length of a URL
        public string YoutubeResultHyperlink { get; set; }

        [MaxLength(2083)]
        public string ThumbnailUrl { get; set; }
 




    }
}
