using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Entities.Processed_LeagueTeamEntities;
using LolMatchFilterNew.Domain.Entities.Processed_ProPlayerEntities;
using LolMatchFilterNew.Domain.Entities.Import_YoutubeDataEntities;


namespace LolMatchFilterNew.Domain.Entities.Import_ScoreboardGamesEntities
{

    public class Import_ScoreboardGamesEntity
    {
        // Game Ids are in format: "LEC/2024 Season/Spring Season_Week 2_10_1
        // Primary key for ScoreboardPlayers, ScoreboardGames & ScoreboardTeams
        [Key]
        public string LeaguepediaGameIdAndTitle { get; set; }

        public string? GameName { get; set; } // ScoreboardGames


        public string? League {get; set;}


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

        public virtual Import_YoutubeDataEntity? YoutubeVideo { get; set; }

 
        public virtual ICollection<Processed_ProPlayerEntity>? Players { get; set; }


        public virtual ICollection<Processed_ProPlayerEntity>? Team1PlayersNav { get; set; }
        

        public virtual ICollection<Processed_ProPlayerEntity>? Team2PlayersNav { get; set; }






    }
}
