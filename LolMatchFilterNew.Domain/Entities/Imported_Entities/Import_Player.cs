using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_Players
{
    public class Import_Player
    {

        public string PageName { get; set; }  // Renamed from _pageName
        [Key]
        public string Id { get; set; }

        public string OverviewPage { get; set; }

        public string Player { get; set; }

        public string? Image { get; set; }

        public string? Name { get; set; }

        public string? NativeName { get; set; }

        public string? NameAlphabet { get; set; }

        public string? NameFull { get; set; }

        public string? Country { get; set; }

        public string? Nationality { get; set; }  // List of String in DB

        public string? NationalityPrimary { get; set; }

        public int? Age { get; set; }

        public DateTime? Birthdate { get; set; }

        public DateTime? Deathdate { get; set; }

        public string? ResidencyFormer { get; set; }

        public string? Team { get; set; }

        public string? Team2 { get; set; }

        public string? CurrentTeams { get; set; }  // List of String in DB

        public string? TeamSystem { get; set; }

        public string? Team2System { get; set; }

        public string? Residency { get; set; }

        public string? Role { get; set; }

        public DateTime? Contract { get; set; }

        public string? FavChamps { get; set; }  // List of String in DB

        public string? SoloqueueIds { get; set; }

        public string? Askfm { get; set; }

        public string? Bluesky { get; set; }

        public string? Discord { get; set; }

        public string? Facebook { get; set; }

        public string? Instagram { get; set; }

        public string? Lolpros { get; set; }

        public string? Reddit { get; set; }

        public string? Snapchat { get; set; }

        public string? Stream { get; set; }

        public string? Twitter { get; set; }

        public string? Vk { get; set; }

        public string? Website { get; set; }

        public string? Weibo { get; set; }

        public string? Youtube { get; set; }

        public string? TeamLast { get; set; }

        public string? RoleLast { get; set; }  // List of String in DB

        public bool? IsRetired { get; set; }

        public bool? ToWildrift { get; set; }

        public bool? IsPersonality { get; set; }

        public bool? IsSubstitute { get; set; }

        public bool? IsTrainee { get; set; }

        public bool? IsLowercase { get; set; }

        public bool? IsAutoTeam { get; set; }

        public bool? IsLowContent { get; set; }
    }
}

