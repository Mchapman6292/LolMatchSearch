using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using LolMatchFilterNew.Domain.DTOs;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using System.Numerics;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_ScoreboardGamesEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRedirectEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRenameEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamsTableEntities;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;

using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_LeagueTeamEntities;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_ProPlayerEntities;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_YoutubeDataEntities;

namespace LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext
{

    // Drop database and add again!!!
    public class MatchFilterDbContext : DbContext, IMatchFilterDbContext
    {
        public MatchFilterDbContext(DbContextOptions<MatchFilterDbContext> options)
            : base(options)
        {
        }
        // Tables taken directly from Leaguepedia, columns & data match exactly. 
        public DbSet<Import_YoutubeDataEntity> Import_YoutubeData { get; set; }
        public DbSet<Import_TeamsTableEntity> Import_TeamsTable { get; set; }
        public DbSet<Import_ScoreboardGamesEntity> Import_ScoreboardGames { get; set; }
        public DbSet<Import_TeamRenameEntity> Import_TeamRename { get; set; }


        public DbSet<Processed_ProPlayerEntity> Processed_ProPlayer { get; set; }
        public DbSet<Processed_LeagueTeamEntity> Processed_LeagueTeam { get; set; }
        public DbSet<Processed_TeamNameHistoryEntity> Processed_TeamNameHistory { get; set; }
        public DbSet<Processed_YoutubeDataEntity> YoutubeMatchExtracts { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Import_YoutubeDataEntity>(entity =>
            {
                entity.ToTable("Import_YoutubeData");
                entity.HasKey(e => e.YoutubeVideoId);
                entity.Property(e => e.YoutubeVideoId).HasComment("Can begin with uppercase letters, numbers, lowercase letters, - and _ , appending single quotation to handle this.").IsRequired();
                entity.Property(e => e.VideoTitle).IsRequired().HasMaxLength(255);
                entity.Property(e => e.PlaylistId).HasMaxLength(100);
                entity.Property(e => e.PlaylistTitle).IsRequired().HasMaxLength(255);
                entity.Property(e => e.PublishedAt_utc).IsRequired();
                entity.Property(e => e.YoutubeResultHyperlink).IsRequired().HasMaxLength(2083);
                entity.Property(e => e.ThumbnailUrl).IsRequired().HasMaxLength(2083);
                entity.Property(e => e.LeaguepediaGameIdAndTitle).IsRequired().HasMaxLength(255);
            });

            modelBuilder.Entity<Import_ScoreboardGamesEntity>(entity =>
            {
                entity.ToTable("Import_ScoreboardGames");
                entity.HasKey(e => e.LeaguepediaGameIdAndTitle);
                entity.Property(e => e.LeaguepediaGameIdAndTitle).IsRequired().HasMaxLength(255);
                entity.Property(e => e.GameName).HasMaxLength(255);
                entity.Property(e => e.League).HasMaxLength(100);
                entity.Property(e => e.DateTime_utc).IsRequired();
                entity.Property(e => e.Tournament).HasMaxLength(255);
                entity.Property(e => e.Team1).HasMaxLength(100);
                entity.Property(e => e.Team2).HasMaxLength(100);
                entity.Property(e => e.Team1Players).HasMaxLength(500);
                entity.Property(e => e.Team2Players).HasMaxLength(500);
                entity.Property(e => e.Team1Picks).HasMaxLength(255);
                entity.Property(e => e.Team2Picks).HasMaxLength(255);
                entity.Property(e => e.WinTeam).HasMaxLength(100);
                entity.Property(e => e.LossTeam).HasMaxLength(100);
            });

            modelBuilder.Entity<Import_TeamRenameEntity>(entity =>
            {
                entity.ToTable("Import_TeamRenames");
                entity.HasKey(t => new { t.OriginalName, t.NewName, t.Date });
                entity.Property(e => e.OriginalName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.NewName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Verb).HasMaxLength(50);
                entity.Property(e => e.IsSamePage).HasMaxLength(10);
                entity.Property(e => e.NewsId).HasMaxLength(100);
            });


            modelBuilder.Entity<Import_TeamsTableEntity>(entity =>
            {
                entity.ToTable("Import_TeamsTable");
                entity.HasKey(t => t.Name);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.OverviewPage).HasMaxLength(2083);
                entity.Property(e => e.Short).HasMaxLength(50);
                entity.Property(e => e.Location).HasMaxLength(100);
                entity.Property(e => e.TeamLocation).HasMaxLength(100);
                entity.Property(e => e.Region).HasMaxLength(50);
                entity.Property(e => e.OrganizationPage).HasMaxLength(2083);
                entity.Property(e => e.Image).HasMaxLength(2083);
                entity.Property(e => e.Twitter).HasMaxLength(255);
                entity.Property(e => e.Youtube).HasMaxLength(255);
                entity.Property(e => e.Facebook).HasMaxLength(255);
                entity.Property(e => e.Instagram).HasMaxLength(255);
                entity.Property(e => e.Discord).HasMaxLength(255);
                entity.Property(e => e.Snapchat).HasMaxLength(255);
                entity.Property(e => e.Vk).HasMaxLength(255);
                entity.Property(e => e.Subreddit).HasMaxLength(255);
                entity.Property(e => e.Website).HasColumnType("text");
                entity.Property(e => e.RosterPhoto).HasMaxLength(2083);
                entity.Property(e => e.IsDisbanded);
                entity.Property(e => e.RenamedTo).HasMaxLength(255);
                entity.Property(e => e.IsLowercase);
            });






            modelBuilder.Entity<Processed_ProPlayerEntity>(entity =>
            {
                entity.ToTable("Processed_ProPlayers");
                entity.HasKey(e => e.LeaguepediaPlayerAllName);
                entity.Property(e => e.LeaguepediaPlayerAllName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.LeaguepediaPlayerId).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CurrentTeam).HasMaxLength(100);
                entity.Property(e => e.InGameName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PreviousInGameNames).HasMaxLength(500);
                entity.Property(e => e.RealName).HasMaxLength(255);
                entity.Property(e => e.Role).HasMaxLength(50);
            });

            modelBuilder.Entity<Processed_LeagueTeamEntity>(entity =>
            {
                entity.ToTable("Processed_LeagueTeamEntity");
                entity.HasKey(e => e.TeamName);
                entity.Property(e => e.NameShort).IsRequired();
                entity.Property(e => e.Region).IsRequired();


            });

      

            modelBuilder.Entity<Processed_TeamNameHistoryEntity>(entity =>
            {
                entity.ToTable("Processed_TeamNameHistory");
                entity.HasKey(t => t.CurrentTeamName);
                entity.Property(e => e.CurrentTeamName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.NameHistory).HasMaxLength(1000);
            });

         

            modelBuilder.Entity<Processed_YoutubeDataEntity>(entity =>
            {
                entity.ToTable("Processed_YoutubeData");
                entity.HasKey(e => e.YoutubeVideoId);
                entity.Property(e => e.YoutubeVideoId).HasComment("Can begin with uppercase letters, numbers, lowercase letters, - and _ , appending single quotation to handle this.").IsRequired();
                entity.Property(e => e.VideoTitle).IsRequired().HasMaxLength(255);
                entity.Property(e => e.PlayListId).HasMaxLength(100);
                entity.Property(e => e.PlayListTitle).HasMaxLength(255);
                entity.Property(e => e.PublishedAt_utc).IsRequired();
                entity.Property(e => e.Team1Short).HasMaxLength(50);
                entity.Property(e => e.Team1Long).HasMaxLength(100);
                entity.Property(e => e.Team2Short).HasMaxLength(50);
                entity.Property(e => e.Team2Long).HasMaxLength(100);
                entity.Property(e => e.Tournament).HasMaxLength(255);
                entity.Property(e => e.GameWeekIdentifier).HasMaxLength(10);
                entity.Property(e => e.GameDayIdentifier).HasMaxLength(10);
                entity.Property(e => e.Season).HasMaxLength(50);
                entity.Property(e => e.IsSeries);
                // GameNumber doesn't need specific configuration as it's a nullable int
            });
        }





    }
}
