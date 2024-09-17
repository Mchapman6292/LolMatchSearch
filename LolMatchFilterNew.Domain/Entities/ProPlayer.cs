using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Entities.ProPlayers
{
    public class ProPlayer
    {
        [Key]
        // 
        public string LeaguepediaPlayerId { get; set; }

        //  PlayerRedirects table all name will include players real name in cases of duplication e.g  	Odin (Shuai Wang).
        public string LeaguepediaPlayerAllName { get; set; } 
        public string InGameName { get; set; }
        public string RealName { get; set; }
        public string Team { get; set; }
        public string Role { get; set; }


    }
}
