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
using LolMatchFilterNew.Domain.Entities.ProPlayers;

namespace LolMatchFilterNew.Infrastructure.DbContexts
{
    public class LolMatchFilterDbContext : DbContext
    {
        public LolMatchFilterDbContext(DbContextOptions<LolMatchFilterDbContext> options)
            : base(options)
        {
        }

        public DbSet<YoutubeVideoEntity> YoutubeVideoResults { get; set; }
        public DbSet<ProPlayer> ProPlayers { get; set; }
        public DbSet<LeaguepediaMatchDetailEntity> LeaguepediaMatchDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<YoutubeVideoEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.Property(e => e.YoutubeResultHyperlink).IsRequired();
            });

            modelBuilder.Entity<ProPlayer>(entity =>
            {
                entity.HasKey(e => e.LeaguepediaId);
                entity.Property(e => e.InGameName).IsRequired();
            });

            modelBuilder.Entity<LeaguepediaMatchDetailEntity>(entity =>
            {
                entity.HasKey(e => e.LeaguepediaGameIdAndTitle);
                entity.Property(e => e.DateTimeUTC).IsRequired();
                entity.Property(e => e.Tournament).IsRequired();
                entity.Property(e => e.Team1).IsRequired();
                entity.Property(e => e.Team2).IsRequired();

                entity.HasMany(e => e.Players)
                    .WithMany()
                    .UsingEntity(j => j.ToTable("MatchDetailProPlayers"));
            });
        }
    }
}
