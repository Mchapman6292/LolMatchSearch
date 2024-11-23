using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Entities.Processed_TeamRenameEntities
{
    public class Processed_TeamRenameEntity
    {
        [Key, Column(Order = 0)]
        [Required]

        public string OriginalName { get; set; }

        [Key, Column(Order = 1)]
        [Required]
        public string NewName { get; set; }

        [Key, Column(Order = 2)]
        [Required]
        public DateTime ChangeDate_utc { get; set; }


        public string? Verb { get; set; }


        public string? IsSamePage { get; set; }

        public string? NewsId { get; set; }
    }
}
