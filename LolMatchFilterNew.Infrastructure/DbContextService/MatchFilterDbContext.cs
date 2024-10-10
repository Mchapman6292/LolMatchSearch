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
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityColumn(); // Auto-incremented id column
                entity.Property(e => e.YoutubeResultHyperlink).IsRequired();

            });


            modelBuilder.Entity<ProPlayerEntity>(entity =>
            {
                entity.HasKey(e => e.LeaguepediaPlayerAllName);
                entity.Property(e => e.InGameName).IsRequired();
                entity.Property(e => e.PreviousInGameNames)
                    // Used to store the list of previous player names as a comma separated string. 
                    .HasConversion(
                        v => string.Join(',', v ?? new List<string>()),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                    );
                entity.HasOne(p => p.CurrentTeamNavigation)
                   .WithMany(t => t.CurrentPlayers) // WithMany Defines the reverse relationship, each team(CurrentTeamNavigation) can have many current players.
                   .HasForeignKey(p => p.CurrentTeam)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(p => p.PreviousTeams)
                   .WithMany(t => t.FormerPlayers)
                   .UsingEntity(j => j.ToTable("TeamFormerPlayers"));
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

                entity.HasOne(e => e.Team1Navigation)
                    .WithMany()
                    .HasForeignKey("Team1NavigationTeamName")
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.Team2Navigation)
                    .WithMany()
                    .HasForeignKey("Team2NavigationTeamName")
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);
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
