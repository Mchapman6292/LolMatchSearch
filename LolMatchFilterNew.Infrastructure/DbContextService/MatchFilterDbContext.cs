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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<YoutubeVideoEntity>(entity =>
            {
                entity.HasKey(e => e.YoutubeVideoId);
                entity.Property(e => e.YoutubeResultHyperlink).IsRequired();

            });


            modelBuilder.Entity<ProPlayerEntity>(entity =>
            {
                entity.HasKey(e => e.LeaguepediaPlayerAllName);
                entity.Property(e => e.InGameName).IsRequired();
                entity.Property(e => e.PreviousInGameNames);
                       

                entity.HasOne(p => p.CurrentTeamNavigation)
                   .WithMany(t => t.CurrentPlayers) // WithMany defines the reverse relationship, each team(CurrentTeamNavigation) can have many current players.
                   .HasForeignKey(p => p.CurrentTeam)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(p => p.PreviousTeams)
                   .WithMany(t => t.FormerPlayers)
                   .UsingEntity(j => j.ToTable("TeamFormerPlayers"));

                entity.HasMany(p => p.Matches)
                  .WithMany(m => m.Players)
                  .UsingEntity(j => j.ToTable("MatchPlayers"));
            });




            modelBuilder.Entity<LeaguepediaMatchDetailEntity>(entity =>
            {
                entity.HasKey(e => e.LeaguepediaGameIdAndTitle);
                entity.Property(e => e.DateTimeUTC).IsRequired();
                entity.Property(e => e.Tournament).IsRequired();
                entity.Property(e => e.Team1).IsRequired();
                entity.Property(e => e.Team2).IsRequired();

                entity.HasOne(e => e.YoutubeVideo)
                    .WithOne(y => y.LeaguepediaMatch)
                    .HasForeignKey<YoutubeVideoEntity>(y => y.LeaguepediaGameIdAndTitle)
                    .IsRequired(false);


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



            modelBuilder.Entity<LeagueTeamEntity>(entity =>
            {
                entity.HasKey(e => e.TeamName);

                entity.HasMany(t => t.CurrentPlayers)
                    .WithOne(p => p.CurrentTeamNavigation)
                    .HasForeignKey(p => p.CurrentTeam)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(t => t.FormerPlayers)
                    .WithMany(p => p.PreviousTeams)
                    .UsingEntity(j => j.ToTable("TeamFormerPlayers"));
            });
            
        }

    }
}
