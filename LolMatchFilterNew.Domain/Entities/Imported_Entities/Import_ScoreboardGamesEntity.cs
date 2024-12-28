using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_ScoreboardGamesEntities
{

    public class Import_ScoreboardGamesEntity
    {
        // Game Ids are in format: "LEC/2024 Season/Spring Season_Week 2_10_1
        // Primary key for ScoreboardPlayers, ScoreboardGamesId & ScoreboardTeams


        [Key]
        public string GameName { get; set; } // ScoreboardGamesId

        [Key]

        // References the individual match in the series e.g 2012 MLG Pro Circuit/Fall/Championship_Round 2_1_2
        public string GameId { get; set; }


        // References the series e.g 2012 MLG Pro Circuit/Fall/Championship_Round 2_1
        public string? MatchId {  get; set; }



        public DateTime? DateTime_utc { get; set; } // ScoreboardPlayers

        public string? Tournament { get; set; } // ScoreboardPlayers


        public string? Team1 { get; set; } // ScoreboardGamesId


        public string? Team2 { get; set; } // ScoreboardGamesId

        public string? Team1Players { get; set; } // ScoreboardGamesId


        public string? Team2Players { get; set; } // ScoreboardGamesId

        public string? Team1Picks { get; set; } // ScoreboardGamesId


        public string? Team2Picks { get; set; } // ScoreboardGamesId


        public string? WinTeam { get; set; } // ScoreboardGamesId


        public string? LossTeam { get; set; } // ScoreboardGamesId

        public int? Team1Kills { get; set; } // ScoreboardGamesId


        public int? Team2Kills { get; set; } // ScoreboardGamesId







    }
}
