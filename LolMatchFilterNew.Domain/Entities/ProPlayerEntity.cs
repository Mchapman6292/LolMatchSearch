using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Entities.LeagueTeamEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace LolMatchFilterNew.Domain.Entities.ProPlayerEntities
{
    public class ProPlayerEntity
    {
        [Key]

        // Use LeaguepediaPlayerAllName as key as PlayerId will be the same for different players with same name.
        // LeaguepediaPlayerAllName will provide in game name & full name in format: Odin (Shuai Wang)
        public string LeaguepediaPlayerAllName { get; set; }
        public string LeaguepediaPlayerId { get; set; }

        public string CurrentTeam { get; set; }

        [Required]
        public string InGameName { get; set; }
        public string PreviousInGameNames { get; set; }
        public string RealName { get; set; }
        public string Role { get; set; }

        public virtual ICollection<LeaguepediaMatchDetailEntity> Matches { get; set; }





    }
}
