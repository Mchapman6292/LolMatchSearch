using LolMatchFilterNew.Domain.Entities.Processed_ProPlayerEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Entities.Import_TeamsTableEntities
{
    public class Import_TeamsTableEntity
    {
        [Key]
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(2083)] 
        public string? OverviewPage { get; set; }

        [MaxLength(50)]
        public string? Short { get; set; }

        [MaxLength(100)]
        public string? Location { get; set; }

        [MaxLength(100)]
        public string? TeamLocation { get; set; }

        [MaxLength(50)]
        public string? Region { get; set; }

        [MaxLength(2083)]
        public string? OrganizationPage { get; set; }

        [MaxLength(2083)]
        public string? Image { get; set; }

        [MaxLength(255)]
        public string? Twitter { get; set; }

        [MaxLength(255)]
        public string? Youtube { get; set; }

        [MaxLength(255)]
        public string? Facebook { get; set; }

        [MaxLength(255)]
        public string? Instagram { get; set; }

        [MaxLength(255)]
        public string? Discord { get; set; }

        [MaxLength(255)]
        public string? Snapchat { get; set; }

        [MaxLength(255)]
        public string? Vk { get; set; }

        [MaxLength(255)]
        public string? Subreddit { get; set; }

        [Column(TypeName = "text")]
        public string? Website { get; set; }

        [MaxLength(2083)]
        public string? RosterPhoto { get; set; }

        public bool IsDisbanded { get; set; }

        [MaxLength(255)]
        public string? RenamedTo { get; set; }

        public bool IsLowercase { get; set; }

    }
}
