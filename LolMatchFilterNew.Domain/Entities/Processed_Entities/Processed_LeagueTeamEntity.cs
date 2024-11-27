
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_LeagueTeamEntities
{
    public class Processed_LeagueTeamEntity
    {

        [Key]
        public string TeamName { get; set; } // Unique identifier for the team

        public string NameShort { get; set; }

        public string Region { get; set; }


    }
}