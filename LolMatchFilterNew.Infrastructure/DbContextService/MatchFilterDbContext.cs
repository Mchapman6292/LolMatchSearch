using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using LolMatchFilterNew.Domain.DTOs;
using LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities;
using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using LolMatchFilterNew.Domain.Entities.ProPlayerEntities;
using LolMatchFilterNew.Domain.Entities.LeagueTeamEntities;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Domain.Entities.YoutubePlaylistEntities;
using System.Numerics;
using LolMatchFilterNew.Domain.Entities.TeamRenamesEntities;
using LolMatchFilterNew.Domain.Entities.TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Entities.LpediaTeamEntities;
using LolMatchFilterNew.Domain.Entities.YoutubeMatchExtractEntities;

namespace LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext
{
    public class MatchFilterDbContext : DbContext, IMatchFilterDbContext
    {
        public MatchFilterDbContext(DbContextOptions<MatchFilterDbContext> options)
            : base(options)
        {
        }

        public DbSet<YoutubeVideoEntity> YoutubeVideoResults { get; set; }
        public DbSet<ProPlayerEntity> ProPlayers { get; set; }
        public DbSet<LeaguepediaMatchDetailEntity> LeaguepediaMatchDetails { get; set; }
        public DbSet<LeagueTeamEntity> Teams { get; set; }
        public DbSet<YoutubePlaylistEntity> YoutubePlaylists { get; set; }
        public DbSet<TeamRenameEntity> TeamRenames { get; set; }
        public DbSet<TeamNameHistoryEntity> TeamNameHistory { get; set; }
        public DbSet<LpediaTeamEntity> LOLTeams { get; set; }
        public DbSet<YoutubeMatchExtractEntity> YoutubeMatchExtracts { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<YoutubeVideoEntity>(entity =>
            {
                entity.ToTable("YoutubeVideoResults");
                entity.HasKey(e => e.YoutubeVideoId);

                entity.Property(e => e.VideoTitle)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PublishedAt)
                    .IsRequired();
                entity.Property(e => e.YoutubeResultHyperlink).IsRequired();

                entity.Property(e => e.ThumbnailUrl)
                   .IsRequired(false)
                   .HasMaxLength(2083);

                entity.Property(e => e.PlaylistId)
                    .IsRequired(false);


                entity.Property(e => e.PlaylistTitle)
                    .IsRequired(false)  
                    .HasMaxLength(255);  

                entity.Property(e => e.LeaguepediaGameIdAndTitle)
                      .IsRequired(false);


                entity.HasOne(e => e.LeaguepediaMatch)
                      .WithOne(l => l.YoutubeVideo)
                      .HasForeignKey<YoutubeVideoEntity>(
                          y => y.LeaguepediaGameIdAndTitle)
                      .IsRequired(false);

                entity.HasOne(e => e.MatchExtract)
                      .WithOne(m => m.YoutubeVideo)
                      .HasForeignKey<YoutubeMatchExtractEntity>(m => m.YoutubeVideoId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<LeaguepediaMatchDetailEntity>(entity =>
            {
                entity.HasKey(e => e.LeaguepediaGameIdAndTitle);
                entity.Property(e => e.DateTimeUTC).IsRequired();
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



            modelBuilder.Entity<ProPlayerEntity>(entity =>
            {
                entity.HasKey(e => e.LeaguepediaPlayerAllName);
                entity.Property(e => e.InGameName).IsRequired();
                entity.Property(e => e.PreviousInGameNames);
                entity.Property(p => p.CurrentTeam).IsRequired(false);
                entity.HasMany(p => p.Matches)
                    .WithMany(m => m.Players)
                    .UsingEntity(j => j.ToTable("MatchPlayers"));
            });

            modelBuilder.Entity<LeagueTeamEntity>(entity =>
            {
                entity.HasKey(e => e.TeamName);
                entity.Property(e => e.NameShort).IsRequired();
                entity.Property(e => e.Region).IsRequired();


            });

            modelBuilder.Entity<TeamRenameEntity>(entity =>
            {
                entity.HasKey(t => new { t.OriginalName, t.NewName, t.Date });
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.OriginalName).IsRequired();
                entity.Property(e => e.NewName).IsRequired();

            });

            modelBuilder.Entity<TeamNameHistoryEntity>(entity =>
            {
                entity.HasKey(t => t.CurrentTeamName);
            });

            modelBuilder.Entity<LpediaTeamEntity>(entity =>
            {
                entity.HasKey(t => t.Name);
            });

            modelBuilder.Entity<YoutubeMatchExtractEntity>(entity =>
            {
                entity.HasKey(e => e.YoutubeVideoId);
                entity.HasOne(e => e.YoutubeVideo)
                    .WithOne(y => y.MatchExtract)
                    .HasForeignKey<YoutubeMatchExtractEntity>(e => e.YoutubeVideoId);
            });
        }

    }
}
