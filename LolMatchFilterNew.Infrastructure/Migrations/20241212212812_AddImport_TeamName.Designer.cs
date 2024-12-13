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
    [Migration("20241212212812_AddImport_TeamName")]
    partial class AddImport_TeamName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_ScoreboardGamesEntities.Import_ScoreboardGamesEntity", b =>
                {
                    b.Property<string>("GameName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("GameId")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime?>("DateTime_utc")
                        .HasMaxLength(100)
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LossTeam")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("MatchId")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Team1")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("Team1Kills")
                        .HasColumnType("integer");

                    b.Property<string>("Team1Picks")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Team1Players")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Team2")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("Team2Kills")
                        .HasColumnType("integer");

                    b.Property<string>("Team2Picks")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Team2Players")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Tournament")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("WinTeam")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("GameName", "GameId");

                    b.ToTable("Import_ScoreboardGames", (string)null);
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRedirectEntities.Import_TeamRedirectEntity", b =>
                {
                    b.Property<string>("PageName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("AllName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("OtherName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("UniqueLine")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("PageName", "AllName");

                    b.ToTable("Import_TeamRedirect", (string)null);
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRenameEntities.Import_TeamRenameEntity", b =>
                {
                    b.Property<string>("OriginalName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("NewName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime?>("Date")
                        .HasMaxLength(10)
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("IsSamePage")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("NewsId")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Verb")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("OriginalName", "NewName", "Date");

                    b.ToTable("Import_TeamRenames", (string)null);
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_Teamnames.Import_TeamnameEntity", b =>
                {
                    b.Property<string>("TeamnameId")
                        .HasColumnType("text");

                    b.Property<List<string>>("Inputs")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("text[]");

                    b.Property<string>("Longname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Medium")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Short")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("TeamnameId");

                    b.ToTable("Import_Teamname", (string)null);
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamsTableEntities.Import_TeamsTableEntity", b =>
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

                    b.ToTable("Import_TeamsTable", (string)null);
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities.Import_YoutubeDataEntity", b =>
                {
                    b.Property<string>("YoutubeVideoId")
                        .HasColumnType("text")
                        .HasComment("Can begin with uppercase letters, numbers, lowercase letters, - and _ , appending single quotation to handle this.");

                    b.Property<string>("PlaylistId")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PlaylistTitle")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime?>("PublishedAt_utc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ThumbnailUrl")
                        .HasMaxLength(2083)
                        .HasColumnType("character varying(2083)");

                    b.Property<string>("VideoTitle")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("YoutubeResultHyperlink")
                        .HasMaxLength(2083)
                        .HasColumnType("character varying(2083)");

                    b.HasKey("YoutubeVideoId");

                    b.ToTable("Import_YoutubeData", (string)null);
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_LeagueTeamEntities.Processed_LeagueTeamEntity", b =>
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

                    b.ToTable("Processed_LeagueTeamEntity", (string)null);
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_ProPlayerEntities.Processed_ProPlayerEntity", b =>
                {
                    b.Property<string>("LeaguepediaPlayerAllName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("CurrentTeam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("InGameName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("LeaguepediaPlayerId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PreviousInGameNames")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("RealName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("LeaguepediaPlayerAllName");

                    b.ToTable("Processed_ProPlayers", (string)null);
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_TeamNameHistoryEntities.Processed_TeamNameHistoryEntity", b =>
                {
                    b.Property<string>("CurrentTeamName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("NameHistory")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.HasKey("CurrentTeamName");

                    b.ToTable("Processed_TeamNameHistory", (string)null);
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_YoutubeDataEntities.Processed_YoutubeDataEntity", b =>
                {
                    b.Property<string>("YoutubeVideoId")
                        .HasColumnType("text")
                        .HasComment("Can begin with uppercase letters, numbers, lowercase letters, - and _ , appending single quotation to handle this.");

                    b.Property<string>("GameDayIdentifier")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<int?>("GameNumber")
                        .HasColumnType("integer");

                    b.Property<string>("GameWeekIdentifier")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<bool>("IsSeries")
                        .HasColumnType("boolean");

                    b.Property<string>("PlayListId")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PlayListTitle")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("PublishedAt_utc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Season")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Team1Long")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Team1Short")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Team2Long")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Team2Short")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Tournament")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("VideoTitle")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("YoutubeVideoId");

                    b.ToTable("Processed_YoutubeData", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
