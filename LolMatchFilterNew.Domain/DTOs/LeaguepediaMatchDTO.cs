using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using LolMatchFilterNew.Domain.Entities.ProPlayerEntities;
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

        public ICollection<ProPlayerEntity> Team1Players { get; set; }

        public virtual ICollection<ProPlayerEntity> Team2Players { get; set; }



    }
}
