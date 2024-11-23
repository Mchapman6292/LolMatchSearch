using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using LolMatchFilterNew.Domain.DTOs;
using LolMatchFilterNew.Domain.Entities.Import_YoutubeDataEntities;
using LolMatchFilterNew.Domain.Entities.Import_ScoreboardGamesEntities;
using LolMatchFilterNew.Domain.Entities.Processed_ProPlayerEntities;
using LolMatchFilterNew.Domain.Entities.Processed_LeagueTeamEntities;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Domain.Entities.Processed_YoutubePlaylistEntities;
using System.Numerics;
using LolMatchFilterNew.Domain.Entities.Processed_TeamRenameEntities;
using LolMatchFilterNew.Domain.Entities.Processed_TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Entities.Import_TeamsTableEntities;
using LolMatchFilterNew.Domain.Entities.YoutubeMatchExtractEntities;

namespace LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext
{
    public class MatchFilterDbContext : DbContext, IMatchFilterDbContext
    {
        public MatchFilterDbContext(DbContextOptions<MatchFilterDbContext> options)
            : base(options)
        {
        }

        public DbSet<Import_YoutubeDataEntity> YoutubeVideoResults { get; set; }
        public DbSet<Processed_ProPlayerEntity> ProPlayers { get; set; }
        public DbSet<Import_ScoreboardGamesEntity> LeaguepediaMatchDetails { get; set; }
        public DbSet<Processed_LeagueTeamEntity> Teams { get; set; }
        public DbSet<Processed_YoutubePlaylistEntity> YoutubePlaylists { get; set; }
        public DbSet<Processed_TeamRenameEntity> TeamRenames { get; set; }
        public DbSet<Processed_TeamNameHistoryEntity> TeamNameHistory { get; set; }
        public DbSet<Import_TeamsTableEntity> LOLTeams { get; set; }
        public DbSet<Processed_YoutubeDataEntity> YoutubeMatchExtracts { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Import_YoutubeDataEntity>(entity =>
            {
                entity.ToTable("YoutubeVideoResults");
                entity.HasKey(e => e.YoutubeVideoId);

                entity.Property(e => e.VideoTitle)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PlaylistId)
                 .HasColumnName("PlaylistName")
                    .IsRequired(false);


                entity.Property(e => e.PlaylistTitle)
                    .HasColumnName("PlaylistTitle")
                    .IsRequired(false);


                entity.Property(e => e.PublishedAt_utc)
                    .IsRequired();

                entity.Property(e => e.YoutubeResultHyperlink).IsRequired();

                entity.Property(e => e.ThumbnailUrl)
                   .IsRequired(false)
                   .HasMaxLength(2083);



                entity.Property(e => e.LeaguepediaGameIdAndTitle)
                      .IsRequired(false);


                entity.HasOne(e => e.LeaguepediaMatch)
                      .WithOne(l => l.YoutubeVideo)
                      .HasForeignKey<Import_YoutubeDataEntity>(
                          y => y.LeaguepediaGameIdAndTitle)
                      .IsRequired(false);

                entity.HasOne(e => e.MatchExtract)
                      .WithOne(m => m.YoutubeVideo)
                      .HasForeignKey<Processed_YoutubeDataEntity>(m => m.YoutubeVideoId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Import_ScoreboardGamesEntity>(entity =>
            {
                entity.HasKey(e => e.LeaguepediaGameIdAndTitle);
                entity.Property(e => e.DateTime_utc).IsRequired();
                entity.Property(e => e.Tournament).IsRequired();
                entity.Property(e => e.Team1).IsRequired();
                entity.Property(e => e.Team2).IsRequired();


                entity.HasMany(m => m.Team1PlayersNav)
                     .WithMany()
                     .UsingEntity(j => j.ToTable("MatchTeam1Players"));

                entity.HasMany(m => m.Team2PlayersNav)
                    .WithMany()
                    .UsingEntity(j => j.ToTable("MatchTeam2Players"));

                entity.HasMany(m => m.Players)
                     .WithMany(p => p.Matches)
                     .UsingEntity(j => j.ToTable("MatchPlayers"));

            });



            modelBuilder.Entity<Processed_ProPlayerEntity>(entity =>
            {
                entity.HasKey(e => e.LeaguepediaPlayerAllName);
                entity.Property(e => e.InGameName).IsRequired();
                entity.Property(e => e.PreviousInGameNames);
                entity.Property(p => p.CurrentTeam).IsRequired(false);
                entity.HasMany(p => p.Matches)
                    .WithMany(m => m.Players)
                    .UsingEntity(j => j.ToTable("MatchPlayers"));
            });

            modelBuilder.Entity<Processed_LeagueTeamEntity>(entity =>
            {
                entity.HasKey(e => e.TeamName);
                entity.Property(e => e.NameShort).IsRequired();
                entity.Property(e => e.Region).IsRequired();


            });

            modelBuilder.Entity<Processed_TeamRenameEntity>(entity =>
            {
                entity.HasKey(t => new { t.OriginalName, t.NewName, t.ChangeDate_utc });
                entity.Property(e => e.ChangeDate_utc).IsRequired();
                entity.Property(e => e.OriginalName).IsRequired();
                entity.Property(e => e.NewName).IsRequired();

            });

            modelBuilder.Entity<Processed_TeamNameHistoryEntity>(entity =>
            {
                entity.HasKey(t => t.CurrentTeamName);
            });

            modelBuilder.Entity<Import_TeamsTableEntity>(entity =>
            {
                entity.HasKey(t => t.Name);
            });

            modelBuilder.Entity<Processed_YoutubeDataEntity>(entity =>
            {
                entity.HasKey(e => e.YoutubeVideoId);
                entity.HasOne(e => e.YoutubeVideo)
                    .WithOne(y => y.MatchExtract)
                    .HasForeignKey<Processed_YoutubeDataEntity>(e => e.YoutubeVideoId);
            });
        }

    }
}
