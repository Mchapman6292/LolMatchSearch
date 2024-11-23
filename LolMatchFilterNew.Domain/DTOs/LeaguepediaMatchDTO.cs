using LolMatchFilterNew.Domain.Entities.Import_ScoreboardGamesEntities;
using LolMatchFilterNew.Domain.Entities.Processed_ProPlayerEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.DTOs.LeaguepediaMatchDTOs
{
    public class LeaguepediaMatchDTO
    {

        public string LeaguepediaGameId { get; set; }
        public string DateTime { get; set; }
        public string Tournament { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public TeamSide Team1Side { get; set; }
        public string Winner { get; set; }

        public ICollection<Processed_ProPlayerEntity> Team1Players { get; set; }

        public virtual ICollection<Processed_ProPlayerEntity> Team2Players { get; set; }



    }
}
