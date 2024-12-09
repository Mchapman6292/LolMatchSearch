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
        // Primary key for ScoreboardPlayers, ScoreboardGames & ScoreboardTeams



        [Key]
        public string GameName { get; set; } // ScoreboardGames

        [Key]
        public string GameId { get; set; }

        public string? League { get; set; }


        public DateTime DateTime_utc { get; set; } // ScoreboardPlayers

        public string? Tournament { get; set; } // ScoreboardPlayers


        public string? Team1 { get; set; } // ScoreboardGames


        public string? Team2 { get; set; } // ScoreboardGames

        public string? Team1Players { get; set; } // ScoreboardGames


        public string? Team2Players { get; set; } // ScoreboardGames

        public string? Team1Picks { get; set; } // ScoreboardGames


        public string? Team2Picks { get; set; } // ScoreboardGames


        public string? WinTeam { get; set; } // ScoreboardGames


        public string? LossTeam { get; set; } // ScoreboardGames

        public int? Team1Kills { get; set; } // ScoreboardGames


        public int? Team2Kills { get; set; } // ScoreboardGames







    }
}
