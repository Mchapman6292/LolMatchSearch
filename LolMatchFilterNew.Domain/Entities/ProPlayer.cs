using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Entities
{
    public class ProPlayer
    {
        public string InGameName { get; set; }
        public string RealName { get; set; }

        public int LeaguepediaId { get; set; }
        public string Team { get; set; }
        public string Role { get; set; }


    }
}
