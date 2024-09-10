using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Entities
{
    public enum TeamSide
    {
        Blue,
        Red
    }

    public class MatchDetail
    {
        public string GameId { get; set; }
        public string DateTime { get; set; }
        public string Tournament { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public TeamSide Team1Side { get; set; }
        public string Winner { get; set; }
        public string Patch { get; set; }
        public List<ProPlayer> Players { get; set; } = new List<ProPlayer>();
    }
}

