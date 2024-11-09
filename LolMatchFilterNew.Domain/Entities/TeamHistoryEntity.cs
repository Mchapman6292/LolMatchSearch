using LolMatchFilterNew.Domain.Entities.LeagueTeamEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Entities.TeamNameHistoryEntities
{
    public class TeamNameHistoryEntity
    {
        [Key]
        public string CurrentTeamName { get; set; }

        public string NameHistory { get; set; }

    }
}