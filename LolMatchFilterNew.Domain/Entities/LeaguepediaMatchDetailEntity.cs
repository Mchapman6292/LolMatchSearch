using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Entities.ProPlayers;

namespace LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities
{
    public enum TeamSide
    {
        Blue,
        Red
    }

    public class LeaguepediaMatchDetailEntity
    {

        // Game Ids are in format: "LEC/2024 Season/Spring Season_Week 2_10_1
        // Primary key for ScoreboardPlayers, ScoreboardGames & ScoreboardTeams
        [Key]
        public string LeaguepediaGameIdAndTitle { get; set; } 
        public string CustomMatchIdji { get; set; }
        public string DateTimeUTC { get; set; } // ScoreboardPlayers
        public string Tournament { get; set; } // ScoreboardPlayers
        public string Team1 { get; set; } // ScoreboardGames
        public string Team2 { get; set; } // ScoreboardGames
        public List<string> team1Picks { get; set; }  // ScoreboardGames
        public List<string> team2Picks { get; set; }  // ScoreboardGames
        public TeamSide Team1Side { get; set; }
        public string Winner { get; set; }

        public virtual ICollection<ProPlayer> Players { get; set; }
    }
}

