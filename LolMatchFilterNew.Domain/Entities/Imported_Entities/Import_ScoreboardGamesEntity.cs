using System.ComponentModel.DataAnnotations;



namespace LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_ScoreboardGamesEntities
{

    public class Import_ScoreboardGamesEntity
    {
        // Game Ids are in format: "LEC/2024 Season/Spring Season_Week 2_10_1
        // Primary key for ScoreboardPlayers, ScoreboardGames & ScoreboardTeams

        [Key]
        public string GameName { get; set; } // ScoreboardGames

        [Key]

        // References the individual match in the series e.g 2012 MLG Pro Circuit/Fall/Championship_Round 2_1_2
        public string GameId { get; set; }


        // References the series e.g 2012 MLG Pro Circuit/Fall/Championship_Round 2_1
        public string? MatchId {  get; set; }


        public DateTime? DateTime_utc { get; set; } // ScoreboardPlayers

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
