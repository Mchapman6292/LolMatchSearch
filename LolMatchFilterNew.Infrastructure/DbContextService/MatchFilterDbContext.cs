// Ignore Spelling: Teamname

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
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_Teamnames;

using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_LeagueTeamEntities;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_ProPlayerEntities;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_YoutubeDataEntities;
using Domain.DTOs.Western_MatchDTOs;

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
        public DbSet<Import_TeamRedirectEntity> Import_TeamRedirect { get; set; }
        public DbSet<Import_TeamnameEntity> Import_Teamname { get; set; }
 


        public DbSet<Processed_ProPlayerEntity> Processed_ProPlayer { get; set; }
        public DbSet<Processed_LeagueTeamEntity> Processed_LeagueTeam { get; set; }
        public DbSet<Processed_YoutubeDataEntity> YoutubeMatchExtracts { get; set; }



        public DbSet<WesternMatchDTO> WesternMatches { get; set; }






        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Import_YoutubeDataEntity>(entity =>
            {
                entity.ToTable("import_youtubedata");
                entity.HasKey(e => e.YoutubeVideoId);
                entity.Property(e => e.YoutubeVideoId).HasColumnName("youtube_video_id").HasComment("Can begin with uppercase letters, numbers, lowercase letters, - and _ , appending single quotation to handle this.").IsRequired();
                entity.Property(e => e.VideoTitle).HasColumnName("video_title").HasMaxLength(255);
                entity.Property(e => e.PlaylistId).HasColumnName("playlist_id").HasMaxLength(100);
                entity.Property(e => e.PlaylistTitle).HasColumnName("playlist_title").HasMaxLength(255);
                entity.Property(e => e.PublishedAt_utc).HasColumnName("published_at_utc");
                entity.Property(e => e.YoutubeResultHyperlink).HasColumnName("youtube_result_hyperlink").HasMaxLength(2083);
                entity.Property(e => e.ThumbnailUrl).HasColumnName("thumbnail_url").HasMaxLength(2083);
            });
            modelBuilder.Entity<Import_ScoreboardGamesEntity>(entity =>
            {
                entity.ToTable("import_scoreboardgames");
                entity.HasKey(e => new { e.GameName, e.GameId });
                entity.Property(e => e.GameName).HasColumnName("game_name").IsRequired().HasMaxLength(255);
                entity.Property(e => e.GameId).HasColumnName("game_id").IsRequired().HasMaxLength(255);
                entity.Property(e => e.MatchId).HasColumnName("match_id").HasMaxLength(255);
                entity.Property(e => e.DateTime_utc).HasColumnName("datetime_utc").HasMaxLength(100);
                entity.Property(e => e.Tournament).HasColumnName("tournament").HasMaxLength(255);
                entity.Property(e => e.Team1).HasColumnName("team1").HasMaxLength(100);
                entity.Property(e => e.Team2).HasColumnName("team2").HasMaxLength(100);
                entity.Property(e => e.Team1Players).HasColumnName("team1_players").HasMaxLength(500);
                entity.Property(e => e.Team2Players).HasColumnName("team2_players").HasMaxLength(500);
                entity.Property(e => e.Team1Picks).HasColumnName("team1_picks").HasMaxLength(255);
                entity.Property(e => e.Team2Picks).HasColumnName("team2_picks").HasMaxLength(255);
                entity.Property(e => e.WinTeam).HasColumnName("win_team").HasMaxLength(100);
                entity.Property(e => e.LossTeam).HasColumnName("loss_team").HasMaxLength(100);
            });

            modelBuilder.Entity<Import_TeamRenameEntity>(entity =>
            {
                entity.ToTable("import_teamrenames");
                entity.HasKey(t => new { t.OriginalName, t.NewName, t.Date });
                entity.Property(e => e.OriginalName).HasColumnName("original_name").IsRequired().HasMaxLength(255);
                entity.Property(e => e.NewName).HasColumnName("new_name").IsRequired().HasMaxLength(255);
                entity.Property(e => e.Date).HasColumnName("date").HasMaxLength(10);
                entity.Property(e => e.Verb).HasColumnName("verb").HasMaxLength(50);
                entity.Property(e => e.IsSamePage).HasColumnName("is_same_page").HasMaxLength(10);
                entity.Property(e => e.NewsId).HasColumnName("news_id").HasMaxLength(100);
            });


            modelBuilder.Entity<Import_TeamsTableEntity>(entity =>
            {
                entity.ToTable("import_teamstable");
                entity.HasKey(t => t.Name);
                entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(255);
                entity.Property(e => e.OverviewPage).HasColumnName("overview_page").HasMaxLength(2083);
                entity.Property(e => e.Short).HasColumnName("short").HasMaxLength(50);
                entity.Property(e => e.Location).HasColumnName("location").HasMaxLength(100);
                entity.Property(e => e.TeamLocation).HasColumnName("team_location").HasMaxLength(100);
                entity.Property(e => e.Region).HasColumnName("region").HasMaxLength(50);
                entity.Property(e => e.OrganizationPage).HasColumnName("organization_page").HasMaxLength(2083);
                entity.Property(e => e.Image).HasColumnName("image").HasMaxLength(2083);
                entity.Property(e => e.Twitter).HasColumnName("twitter").HasMaxLength(255);
                entity.Property(e => e.Youtube).HasColumnName("youtube").HasMaxLength(255);
                entity.Property(e => e.Facebook).HasColumnName("facebook").HasMaxLength(255);
                entity.Property(e => e.Instagram).HasColumnName("instagram").HasMaxLength(255);
                entity.Property(e => e.Discord).HasColumnName("discord").HasMaxLength(255);
                entity.Property(e => e.Snapchat).HasColumnName("snapchat").HasMaxLength(255);
                entity.Property(e => e.Vk).HasColumnName("vk").HasMaxLength(255);
                entity.Property(e => e.Subreddit).HasColumnName("subreddit").HasMaxLength(255);
                entity.Property(e => e.Website).HasColumnName("website").HasColumnType("text");
                entity.Property(e => e.RosterPhoto).HasColumnName("roster_photo").HasMaxLength(2083);
                entity.Property(e => e.IsDisbanded).HasColumnName("is_disbanded");
                entity.Property(e => e.RenamedTo).HasColumnName("renamed_to").HasMaxLength(255);
                entity.Property(e => e.IsLowercase).HasColumnName("is_lowercase");
            });



            modelBuilder.Entity<Import_TeamRedirectEntity>(entity =>
            {
                entity.ToTable("import_teamredirect");
                entity.HasKey(e => new { e.PageName, e.AllName });
                entity.Property(e => e.PageName).HasColumnName("page_name").HasMaxLength(255);
                entity.Property(e => e.AllName).HasColumnName("all_name").HasMaxLength(255);
                entity.Property(e => e.OtherName).HasColumnName("other_name").HasMaxLength(255);
                entity.Property(e => e.UniqueLine).HasColumnName("unique_line").HasMaxLength(255);
            });
            modelBuilder.Entity<Import_TeamnameEntity>(entity =>
            {
                entity.ToTable("import_teamname");
                entity.HasKey(e => e.TeamnameId);
                entity.Property(e => e.TeamnameId).HasColumnName("teamname_id");
                entity.Property(e => e.Longname).HasColumnName("longname").HasMaxLength(100);
                entity.Property(e => e.Short).HasColumnName("short").HasMaxLength(100);
                entity.Property(e => e.Medium).HasColumnName("medium").HasMaxLength(100);
                entity.Property(e => e.Inputs).HasColumnName("inputs").HasMaxLength(1000);
            });




            modelBuilder.Entity<Processed_ProPlayerEntity>(entity =>
            {
                entity.ToTable("processed_proplayers");
                entity.HasKey(e => e.LeaguepediaPlayerAllName);
                entity.Property(e => e.LeaguepediaPlayerAllName).HasColumnName("leaguepedia_player_all_name").IsRequired().HasMaxLength(255);
                entity.Property(e => e.LeaguepediaPlayerId).HasColumnName("leaguepedia_player_id").IsRequired().HasMaxLength(100);
                entity.Property(e => e.CurrentTeam).HasColumnName("current_team").HasMaxLength(100);
                entity.Property(e => e.InGameName).HasColumnName("in_game_name").IsRequired().HasMaxLength(100);
                entity.Property(e => e.PreviousInGameNames).HasColumnName("previous_in_game_names").HasMaxLength(500);
                entity.Property(e => e.RealName).HasColumnName("real_name").HasMaxLength(255);
                entity.Property(e => e.Role).HasColumnName("role").HasMaxLength(50);
            });
            modelBuilder.Entity<Processed_LeagueTeamEntity>(entity =>
            {
                entity.ToTable("processed_leagueteam");
                entity.HasKey(e => e.TeamName);
                entity.Property(e => e.TeamName).HasColumnName("team_name");
                entity.Property(e => e.NameShort).HasColumnName("name_short").IsRequired();
                entity.Property(e => e.Region).HasColumnName("region").IsRequired();
            });



            modelBuilder.Entity<Processed_YoutubeDataEntity>(entity =>
            {
                entity.ToTable("processed_youtubedata");
                entity.HasKey(e => e.YoutubeVideoId);
                entity.Property(e => e.YoutubeVideoId).HasColumnName("youtube_video_id").HasComment("Can begin with uppercase letters, numbers, lowercase letters, - and _ , appending single quotation to handle this.").IsRequired();
                entity.Property(e => e.VideoTitle).HasColumnName("video_title").IsRequired().HasMaxLength(255);
                entity.Property(e => e.PlayListId).HasColumnName("playlist_id").HasMaxLength(100);
                entity.Property(e => e.PlayListTitle).HasColumnName("playlist_title").HasMaxLength(255);
                entity.Property(e => e.PublishedAt_utc).HasColumnName("published_at_utc").IsRequired();
                entity.Property(e => e.Team1Short).HasColumnName("team1_short").HasMaxLength(50);
                entity.Property(e => e.Team1Long).HasColumnName("team1_long").HasMaxLength(100);
                entity.Property(e => e.Team2Short).HasColumnName("team2_short").HasMaxLength(50);
                entity.Property(e => e.Team2Long).HasColumnName("team2_long").HasMaxLength(100);
                entity.Property(e => e.Tournament).HasColumnName("tournament").HasMaxLength(255);
                entity.Property(e => e.GameWeekIdentifier).HasColumnName("game_week_identifier").HasMaxLength(10);
                entity.Property(e => e.GameDayIdentifier).HasColumnName("game_day_identifier").HasMaxLength(10);
                entity.Property(e => e.Season).HasColumnName("season").HasMaxLength(50);
                entity.Property(e => e.IsSeries).HasColumnName("is_series");
                entity.Property(e => e.GameNumber).HasColumnName("game_number");
            });



            modelBuilder.Entity<WesternMatchDTO>(entity =>
            {
                entity.HasNoKey();
            });






        }







    }
}
