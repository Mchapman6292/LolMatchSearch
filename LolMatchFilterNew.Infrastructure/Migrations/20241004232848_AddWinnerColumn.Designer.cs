﻿// <auto-generated />
using System;
using System.Collections.Generic;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    [DbContext(typeof(MatchFilterDbContext))]
    [Migration("20241004232848_AddWinnerColumn")]
    partial class AddWinnerColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LeagueTeamEntityProPlayerEntity", b =>
                {
                    b.Property<string>("FormerPlayersLeaguepediaPlayerAllName")
                        .HasColumnType("text");

                    b.Property<string>("PreviousTeamsTeamName")
                        .HasColumnType("text");

                    b.HasKey("FormerPlayersLeaguepediaPlayerAllName", "PreviousTeamsTeamName");

                    b.HasIndex("PreviousTeamsTeamName");

                    b.ToTable("TeamFormerPlayers", (string)null);
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.LeagueTeamEntities.LeagueTeamEntity", b =>
                {
                    b.Property<string>("TeamName")
                        .HasColumnType("text");

                    b.HasKey("TeamName");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities.LeaguepediaMatchDetailEntity", b =>
                {
                    b.Property<string>("LeaguepediaGameIdAndTitle")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTimeUTC")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ProPlayerEntityLeaguepediaPlayerAllName")
                        .HasColumnType("text");

                    b.Property<string>("Team1")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Team1NavigationTeamName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<string>>("Team1Picks")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("Team2")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Team2NavigationTeamName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<string>>("Team2Picks")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("Tournament")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Winner")
                        .HasColumnType("integer");

                    b.HasKey("LeaguepediaGameIdAndTitle");

                    b.HasIndex("ProPlayerEntityLeaguepediaPlayerAllName");

                    b.HasIndex("Team1NavigationTeamName");

                    b.HasIndex("Team2NavigationTeamName");

                    b.ToTable("LeaguepediaMatchDetails");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.ProPlayerEntities.ProPlayerEntity", b =>
                {
                    b.Property<string>("LeaguepediaPlayerAllName")
                        .HasColumnType("text");

                    b.Property<string>("CurrentTeam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("InGameName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LeaguepediaPlayerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RealName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LeaguepediaPlayerAllName");

                    b.HasIndex("CurrentTeam");

                    b.ToTable("ProPlayers");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities.YoutubeVideoEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("LeaguepediaGameIdAndTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("PublishedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("YoutubeResultHyperlink")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("YoutubeVideoId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LeaguepediaGameIdAndTitle")
                        .IsUnique();

                    b.ToTable("YoutubeVideoResults");
                });

            modelBuilder.Entity("LeagueTeamEntityProPlayerEntity", b =>
                {
                    b.HasOne("LolMatchFilterNew.Domain.Entities.ProPlayerEntities.ProPlayerEntity", null)
                        .WithMany()
                        .HasForeignKey("FormerPlayersLeaguepediaPlayerAllName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LolMatchFilterNew.Domain.Entities.LeagueTeamEntities.LeagueTeamEntity", null)
                        .WithMany()
                        .HasForeignKey("PreviousTeamsTeamName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities.LeaguepediaMatchDetailEntity", b =>
                {
                    b.HasOne("LolMatchFilterNew.Domain.Entities.ProPlayerEntities.ProPlayerEntity", null)
                        .WithMany("Matches")
                        .HasForeignKey("ProPlayerEntityLeaguepediaPlayerAllName");

                    b.HasOne("LolMatchFilterNew.Domain.Entities.LeagueTeamEntities.LeagueTeamEntity", "Team1Navigation")
                        .WithMany()
                        .HasForeignKey("Team1NavigationTeamName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LolMatchFilterNew.Domain.Entities.LeagueTeamEntities.LeagueTeamEntity", "Team2Navigation")
                        .WithMany()
                        .HasForeignKey("Team2NavigationTeamName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team1Navigation");

                    b.Navigation("Team2Navigation");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.ProPlayerEntities.ProPlayerEntity", b =>
                {
                    b.HasOne("LolMatchFilterNew.Domain.Entities.LeagueTeamEntities.LeagueTeamEntity", "CurrentTeamNavigation")
                        .WithMany("CurrentPlayers")
                        .HasForeignKey("CurrentTeam")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("CurrentTeamNavigation");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities.YoutubeVideoEntity", b =>
                {
                    b.HasOne("LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities.LeaguepediaMatchDetailEntity", "LeaguepediaMatch")
                        .WithOne("YoutubeVideo")
                        .HasForeignKey("LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities.YoutubeVideoEntity", "LeaguepediaGameIdAndTitle")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LeaguepediaMatch");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.LeagueTeamEntities.LeagueTeamEntity", b =>
                {
                    b.Navigation("CurrentPlayers");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities.LeaguepediaMatchDetailEntity", b =>
                {
                    b.Navigation("YoutubeVideo")
                        .IsRequired();
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.ProPlayerEntities.ProPlayerEntity", b =>
                {
                    b.Navigation("Matches");
                });
#pragma warning restore 612, 618
        }
    }
}
