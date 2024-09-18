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

namespace LolMatchFilterNew.Infrastructure.DbContexts
{
    public class LolMatchFilterDbContext : DbContext
    {
        public LolMatchFilterDbContext(DbContextOptions<LolMatchFilterDbContext> options)
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

                entity.HasMany(e => e.Team1Navigation)
                .WithMany(p => p.Matches)  
                .UsingEntity(j => j.ToTable("Team1Players"));

                
                entity.HasMany(e => e.Team2Players)
                .WithMany(p => p.Matches)  
                .UsingEntity(j => j.ToTable("Team2Players"));
            });

            modelBuilder.Entity<LeagueTeamEntity>(entity =>
            {
                entity.HasKey(e => e.TeamName);
                entity.Property(e => e.TeamName).IsRequired();

               
                entity.HasMany(t => t.CurrentPlayers)
                    .WithOne(p => p.CurrentTeamNavigation)
                    .HasForeignKey(p => p.CurrentTeam)
                    .OnDelete(DeleteBehavior.SetNull);

                // Relationship with former players
                entity.HasMany(t => t.FormerPlayers)
                    .WithMany(p => p.PreviousTeams)
                    .UsingEntity(j => j.ToTable("TeamFormerPlayers"));


                entity.HasMany(t => t.Matches)
                    .WithOne() 
                    .HasForeignKey(m => m.Team1)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(t => t.Matches)
                    .WithOne()  
                    .HasForeignKey(m => m.Team2)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }

 }

