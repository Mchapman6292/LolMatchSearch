using LolMatchFilterNew.Domain.Entities.Import_YoutubeDataEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Entities.YoutubeMatchExtractEntities
{
    public class Processed_YoutubeDataEntity
    {
        [Key]

        [Comment("Can begin with uppercase letters, numbers, lowercase letters, - and _ , appending single quotation to handle this.")]
        // Can begin with uppercase letters, numbers, lowercase letters, - and _ 
        public string YoutubeVideoId { get; set; }

        [Required]
        [MaxLength(255)]
        public string VideoTitle { get; set; }

        public string? PlayListId { get; set; }   

        public string? PlayListTitle { get; set; }   

        public DateTime PublishedAt_utc { get; set; }

        public string? Team1Short {  get; set; }  
        public string? Team1Long {  get; set; }
        public string? Team2Short { get; set; }
        public string? Team2Long { get; set; }
        public string? Tournament { get; set; }

        // Identifies week x of the split and what day it occurred e.g W5D1 = Week 5 Day 1
        public string? GameWeekIdentifier { get; set; }
        public string? GameDayIdentifier { get; set; }
        public string? Season { get; set; }
        public bool IsSeries { get; set; }  
        public int? GameNumber { get; set; }
    }
}
