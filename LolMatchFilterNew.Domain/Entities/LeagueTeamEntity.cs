﻿using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using LolMatchFilterNew.Domain.Entities.ProPlayerEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Entities.LeagueTeamEntities
{
    public class LeagueTeamEntity
    {
        [Key]
        public string TeamName { get; set; } // Unique identifier for the team

        public virtual ICollection<ProPlayerEntity> CurrentPlayers { get; set; } // Collections are used to define the reverse for these relationships(Each team has current players & former players)

        public virtual ICollection<ProPlayerEntity> FormerPlayers { get; set; }

    }
}