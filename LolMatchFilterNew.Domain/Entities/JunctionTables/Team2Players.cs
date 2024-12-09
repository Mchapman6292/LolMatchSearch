using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_ScoreboardGamesEntities;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_ProPlayerEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Entities.JunctionTables
{
    public class Team2Players
    {
        public string LeaguepediaGameIdAndTitle { get; set; }
        public string LeaguepediaPlayerAllName { get; set; }

        // Navigation properties
        public virtual Import_ScoreboardGamesEntity Match { get; set; }
        public virtual Processed_ProPlayerEntity Player { get; set; }
    }
}
}
