using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_ScoreboardGamesEntities;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_ProPlayerEntities;

namespace LolMatchFilterNew.Domain.Entities.JunctionTables.Team1Playerss
{
    public class Team1Players
    {
        public string GameName { get; set; }
        public string GameId { get; set; }

        // Navigation properties
        public virtual Import_ScoreboardGamesEntity Match { get; set; }
        public virtual Processed_ProPlayerEntity Player { get; set; }
    }
}

