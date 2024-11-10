﻿// <auto-generated />
using System;
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
    [Migration("20241109101020_UpdateLpediaTeams")]
    partial class UpdateLpediaTeams
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LeaguepediaMatchDetailEntityProPlayerEntity", b =>
                {
                    b.Property<string>("LeaguepediaMatchDetailEntityLeaguepediaGameIdAndTitle")
                        .HasColumnType("text");

                    b.Property<string>("Team1PlayersNavLeaguepediaPlayerAllName")
                        .HasColumnType("text");

                    b.HasKey("LeaguepediaMatchDetailEntityLeaguepediaGameIdAndTitle", "Team1PlayersNavLeaguepediaPlayerAllName");

                    b.HasIndex("Team1PlayersNavLeaguepediaPlayerAllName");

                    b.ToTable("MatchTeam1Players", (string)null);
                });

            modelBuilder.Entity("LeaguepediaMatchDetailEntityProPlayerEntity1", b =>
                {
                    b.Property<string>("LeaguepediaMatchDetailEntity1LeaguepediaGameIdAndTitle")
                        .HasColumnType("text");

                    b.Property<string>("Team2PlayersNavLeaguepediaPlayerAllName")
                        .HasColumnType("text");

                    b.HasKey("LeaguepediaMatchDetailEntity1LeaguepediaGameIdAndTitle", "Team2PlayersNavLeaguepediaPlayerAllName");

                    b.HasIndex("Team2PlayersNavLeaguepediaPlayerAllName");

                    b.ToTable("MatchTeam2Players", (string)null);
                });

            modelBuilder.Entity("LeaguepediaMatchDetailEntityProPlayerEntity2", b =>
                {
                    b.Property<string>("MatchesLeaguepediaGameIdAndTitle")
                        .HasColumnType("text");

                    b.Property<string>("PlayersLeaguepediaPlayerAllName")
                        .HasColumnType("text");

                    b.HasKey("MatchesLeaguepediaGameIdAndTitle", "PlayersLeaguepediaPlayerAllName");

                    b.HasIndex("PlayersLeaguepediaPlayerAllName");

                    b.ToTable("MatchPlayers", (string)null);
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.LeagueTeamEntities.LeagueTeamEntity", b =>
                {
                    b.Property<string>("TeamName")
                        .HasColumnType("text");

                    b.Property<string>("NameShort")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TeamName");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities.LeaguepediaMatchDetailEntity", b =>
                {
                    b.Property<string>("LeaguepediaGameIdAndTitle")
                        .HasColumnType("text")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("DateTimeUTC")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(3);

                    b.Property<string>("GameName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(1);

                    b.Property<string>("League")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(2);

                    b.Property<string>("LossTeam")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(12);

                    b.Property<string>("Team1")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(5);

                    b.Property<int>("Team1Kills")
                        .HasColumnType("integer")
                        .HasColumnOrder(13);

                    b.Property<string>("Team1Picks")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(9);

                    b.Property<string>("Team1Players")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(7);

                    b.Property<string>("Team2")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(6);

                    b.Property<int>("Team2Kills")
                        .HasColumnType("integer")
                        .HasColumnOrder(14);

                    b.Property<string>("Team2Picks")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(10);

                    b.Property<string>("Team2Players")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(8);

                    b.Property<string>("Tournament")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(4);

                    b.Property<string>("WinTeam")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(11);

                    b.HasKey("LeaguepediaGameIdAndTitle");

                    b.ToTable("LeaguepediaMatchDetails");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.LpediaTeamEntities.LpediaTeamEntity", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Discord")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Facebook")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Image")
                        .HasMaxLength(2083)
                        .HasColumnType("character varying(2083)");

                    b.Property<string>("Instagram")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("IsDisbanded")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsLowercase")
                        .HasColumnType("boolean");

                    b.Property<string>("Location")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("OrganizationPage")
                        .HasMaxLength(2083)
                        .HasColumnType("character varying(2083)");

                    b.Property<string>("OverviewPage")
                        .HasMaxLength(2083)
                        .HasColumnType("character varying(2083)");

                    b.Property<string>("Region")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("RenamedTo")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("RosterPhoto")
                        .HasMaxLength(2083)
                        .HasColumnType("character varying(2083)");

                    b.Property<string>("Short")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Snapchat")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Subreddit")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("TeamLocation")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Twitter")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Vk")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Website")
                        .HasColumnType("text");

                    b.Property<string>("Youtube")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Name");

                    b.ToTable("LOLTeams");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.ProPlayerEntities.ProPlayerEntity", b =>
                {
                    b.Property<string>("LeaguepediaPlayerAllName")
                        .HasColumnType("text");

                    b.Property<string>("CurrentTeam")
                        .HasColumnType("text");

                    b.Property<string>("InGameName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LeaguepediaPlayerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PreviousInGameNames")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RealName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LeaguepediaPlayerAllName");

                    b.ToTable("ProPlayers");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.TeamNameHistoryEntities.TeamNameHistoryEntity", b =>
                {
                    b.Property<string>("CurrentTeamName")
                        .HasColumnType("text");

                    b.Property<string>("NameHistory")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CurrentTeamName");

                    b.ToTable("TeamNameHistory");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.TeamRenamesEntities.TeamRenameEntity", b =>
                {
                    b.Property<string>("OriginalName")
                        .HasColumnType("text")
                        .HasColumnOrder(0);

                    b.Property<string>("NewName")
                        .HasColumnType("text")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(2);

                    b.Property<string>("IsSamePage")
                        .HasColumnType("text");

                    b.Property<string>("NewsId")
                        .HasColumnType("text");

                    b.Property<string>("Verb")
                        .HasColumnType("text");

                    b.HasKey("OriginalName", "NewName", "Date");

                    b.ToTable("TeamRenames");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.YoutubePlaylistEntities.YoutubePlaylistEntity", b =>
                {
                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<string>("PlaylistId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("name");

                    b.ToTable("YoutubePlaylists");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities.YoutubeVideoEntity", b =>
                {
                    b.Property<string>("YoutubeVideoId")
                        .HasColumnType("text")
                        .HasComment("Can begin with uppercase letters, numbers, lowercase letters, - and _ , appending single quotation to handle this.");

                    b.Property<string>("LeaguepediaGameIdAndTitle")
                        .HasColumnType("text");

                    b.Property<string>("PlaylistName")
                        .HasColumnType("text");

                    b.Property<DateTime>("PublishedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ThumbnailUrl")
                        .HasMaxLength(2083)
                        .HasColumnType("character varying(2083)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("YoutubeResultHyperlink")
                        .IsRequired()
                        .HasMaxLength(2083)
                        .HasColumnType("character varying(2083)");

                    b.HasKey("YoutubeVideoId");

                    b.HasIndex("LeaguepediaGameIdAndTitle")
                        .IsUnique();

                    b.ToTable("YoutubeVideoResults", (string)null);
                });

            modelBuilder.Entity("LeaguepediaMatchDetailEntityProPlayerEntity", b =>
                {
                    b.HasOne("LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities.LeaguepediaMatchDetailEntity", null)
                        .WithMany()
                        .HasForeignKey("LeaguepediaMatchDetailEntityLeaguepediaGameIdAndTitle")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LolMatchFilterNew.Domain.Entities.ProPlayerEntities.ProPlayerEntity", null)
                        .WithMany()
                        .HasForeignKey("Team1PlayersNavLeaguepediaPlayerAllName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LeaguepediaMatchDetailEntityProPlayerEntity1", b =>
                {
                    b.HasOne("LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities.LeaguepediaMatchDetailEntity", null)
                        .WithMany()
                        .HasForeignKey("LeaguepediaMatchDetailEntity1LeaguepediaGameIdAndTitle")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LolMatchFilterNew.Domain.Entities.ProPlayerEntities.ProPlayerEntity", null)
                        .WithMany()
                        .HasForeignKey("Team2PlayersNavLeaguepediaPlayerAllName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LeaguepediaMatchDetailEntityProPlayerEntity2", b =>
                {
                    b.HasOne("LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities.LeaguepediaMatchDetailEntity", null)
                        .WithMany()
                        .HasForeignKey("MatchesLeaguepediaGameIdAndTitle")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LolMatchFilterNew.Domain.Entities.ProPlayerEntities.ProPlayerEntity", null)
                        .WithMany()
                        .HasForeignKey("PlayersLeaguepediaPlayerAllName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities.YoutubeVideoEntity", b =>
                {
                    b.HasOne("LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities.LeaguepediaMatchDetailEntity", "LeaguepediaMatch")
                        .WithOne("YoutubeVideo")
                        .HasForeignKey("LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities.YoutubeVideoEntity", "LeaguepediaGameIdAndTitle");

                    b.Navigation("LeaguepediaMatch");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities.LeaguepediaMatchDetailEntity", b =>
                {
                    b.Navigation("YoutubeVideo")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
