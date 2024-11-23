using LolMatchFilterNew.Domain.Entities.Import_ScoreboardGamesEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LolMatchFilterNew.Domain.Entities.Processed_ProPlayerEntities;
using System.Diagnostics.Contracts;
using Microsoft.EntityFrameworkCore;
using LolMatchFilterNew.Domain.Entities.YoutubeMatchExtractEntities;


namespace LolMatchFilterNew.Domain.Entities.Import_YoutubeDataEntities

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

        public string? PlaylistId { get; set; }

        public string PlaylistTitle { get; set; }


        public DateTime PublishedAt_utc { get; set; }

        [MaxLength(2083)]  // Max length of a URL
        public string YoutubeResultHyperlink { get; set; }

        [MaxLength(2083)]
        public string ThumbnailUrl { get; set; }
        public string LeaguepediaGameIdAndTitle { get; set; }




    }
}
