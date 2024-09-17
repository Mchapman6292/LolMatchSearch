using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities

{
    public class YoutubeVideoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CustomMatchId { get; set; }
        public string YoutubeVideoId { get; set; }
        public string Title { get; set; }
        public DateTime? PublishedAt { get; set; }

        public string YoutubeResultHyperlink { get; set; }

        // Navigation property
        public virtual LeaguepediaMatchDetailEntity leaguepediaMatch { get; set; }

    }
}
