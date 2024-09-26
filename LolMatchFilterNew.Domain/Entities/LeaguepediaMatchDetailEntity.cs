using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Entities.LeagueTeamEntities;
using LolMatchFilterNew.Domain.Entities.ProPlayerEntities;
using LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities;


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

        [Required]
        public DateTime DateTimeUTC { get; set; } // ScoreboardPlayers

        [Required]
        public string Tournament { get; set; } // ScoreboardPlayers

        [Required]
        public string Team1 { get; set; } // ScoreboardGames

        [Required]
        public string Team2 { get; set; } // ScoreboardGames
        public List<string> Team1Picks { get; set; }  // ScoreboardGames
        public List<string> Team2Picks { get; set; }  // ScoreboardGames
        public TeamSide Team1Side { get; set; }
        public string Winner { get; set; }



        public virtual YoutubeVideoEntity YoutubeVideo { get; set; }

        public virtual LeagueTeamEntity Team1Navigation { get; set; }
        public virtual LeagueTeamEntity Team2Navigation { get; set; }


    }
}
