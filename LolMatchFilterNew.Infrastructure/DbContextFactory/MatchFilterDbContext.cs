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

namespace LolMatchFilterNew.Infrastructure.DbContextFactory.MatchFilterDbContexts
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
                entity.HasOne(p => p.CurrentTeamNavigation)
                   .WithMany(t => t.CurrentPlayers)
                   .HasForeignKey(p => p.CurrentTeam)
                   .OnDelete(DeleteBehavior.SetNull);
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
                  .HasForeignKey<YoutubeVideoEntity>(y => y.LeaguepediaGameIdAndTitle);


                modelBuilder.Entity<LeagueTeamEntity>(entity =>
            {
                entity.HasKey(e => e.TeamName);

                entity.HasMany(t => t.CurrentPlayers)
                    .WithOne(p => p.CurrentTeamNavigation)
                    .HasForeignKey(p => p.CurrentTeam)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(t => t.FormerPlayers)
                    .WithMany(p => p.PreviousTeams)
                    .UsingEntity(j => j.ToTable("TeamFormerPlayers"));
            });
            });
        }

    }
}
