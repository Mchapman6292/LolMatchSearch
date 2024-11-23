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
    public enum TeamSide
    {
        Blue,
        Red
    }

    public class Import_ScoreboardGamesEntity
    {
        // Game Ids are in format: "LEC/2024 Season/Spring Season_Week 2_10_1
        // Primary key for ScoreboardPlayers, ScoreboardGames & ScoreboardTeams
        [Key]
        [Column(Order = 0)]
        public string LeaguepediaGameIdAndTitle { get; set; }

        [Column(Order = 1)]
        public string GameName { get; set; } // ScoreboardGames

        [Column(Order = 2)]
        public string League {get; set;}

        [Required]
        [Column(Order = 3)]
        public DateTime DateTimeUTC { get; set; } // ScoreboardPlayers

        [Required]
        [Column(Order = 4)]
        public string Tournament { get; set; } // ScoreboardPlayers

        [Required]
        [Column(Order = 5)]
        public string Team1 { get; set; } // ScoreboardGames

        [Required]
        [Column(Order = 6)]
        public string Team2 { get; set; } // ScoreboardGames

        [Column(Order = 7)]
        public string Team1Players { get; set; } // ScoreboardGames

        [Column(Order = 8)]
        public string Team2Players { get; set; } // ScoreboardGames

        [Column(Order = 9)]
        public string Team1Picks { get; set; } // ScoreboardGames

        [Column(Order = 10)]
        public string Team2Picks { get; set; } // ScoreboardGames

        [Column(Order = 11)]
        public string WinTeam { get; set; } // ScoreboardGames

        [Column(Order = 12)]
        public string LossTeam { get; set; } // ScoreboardGames

        [Column(Order = 13)]
        public int Team1Kills { get; set; } // ScoreboardGames

        [Column(Order = 14)]
        public int Team2Kills { get; set; } // ScoreboardGames

        [Column(Order = 15)]
        public virtual Import_YoutubeDataEntity YoutubeVideo { get; set; }

        [Column(Order = 16)]
        public virtual ICollection<Processed_ProPlayerEntity> Players { get; set; }

        [Column(Order = 17)]
        public virtual ICollection<Processed_ProPlayerEntity> Team1PlayersNav { get; set; }
        
        [Column(Order = 18)]
        public virtual ICollection<Processed_ProPlayerEntity> Team2PlayersNav { get; set; }






    }
}
