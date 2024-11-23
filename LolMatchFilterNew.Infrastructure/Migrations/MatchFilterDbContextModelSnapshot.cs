﻿// <auto-generated />
using System;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    [DbContext(typeof(MatchFilterDbContext))]
    partial class MatchFilterDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Import_ScoreboardGamesEntityProcessed_ProPlayerEntity", b =>
                {
                    b.Property<string>("Import_ScoreboardGamesEntityLeaguepediaGameIdAndTitle")
                        .HasColumnType("text");

                    b.Property<string>("Team1PlayersNavLeaguepediaPlayerAllName")
                        .HasColumnType("text");

                    b.HasKey("Import_ScoreboardGamesEntityLeaguepediaGameIdAndTitle", "Team1PlayersNavLeaguepediaPlayerAllName");

                    b.HasIndex("Team1PlayersNavLeaguepediaPlayerAllName");

                    b.ToTable("MatchTeam1Players", (string)null);
                });

            modelBuilder.Entity("Import_ScoreboardGamesEntityProcessed_ProPlayerEntity1", b =>
                {
                    b.Property<string>("Import_ScoreboardGamesEntity1LeaguepediaGameIdAndTitle")
                        .HasColumnType("text");

                    b.Property<string>("Team2PlayersNavLeaguepediaPlayerAllName")
                        .HasColumnType("text");

                    b.HasKey("Import_ScoreboardGamesEntity1LeaguepediaGameIdAndTitle", "Team2PlayersNavLeaguepediaPlayerAllName");

                    b.HasIndex("Team2PlayersNavLeaguepediaPlayerAllName");

                    b.ToTable("MatchTeam2Players", (string)null);
                });

            modelBuilder.Entity("Import_ScoreboardGamesEntityProcessed_ProPlayerEntity2", b =>
                {
                    b.Property<string>("MatchesLeaguepediaGameIdAndTitle")
                        .HasColumnType("text");

                    b.Property<string>("PlayersLeaguepediaPlayerAllName")
                        .HasColumnType("text");

                    b.HasKey("MatchesLeaguepediaGameIdAndTitle", "PlayersLeaguepediaPlayerAllName");

                    b.HasIndex("PlayersLeaguepediaPlayerAllName");

                    b.ToTable("MatchPlayers", (string)null);
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Import_ScoreboardGamesEntities.Import_ScoreboardGamesEntity", b =>
                {
                    b.Property<string>("LeaguepediaGameIdAndTitle")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTime_utc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GameName")
                        .HasColumnType("text");

                    b.Property<string>("League")
                        .HasColumnType("text");

                    b.Property<string>("LossTeam")
                        .HasColumnType("text");

                    b.Property<string>("Team1")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Team1Kills")
                        .HasColumnType("integer");

                    b.Property<string>("Team1Picks")
                        .HasColumnType("text");

                    b.Property<string>("Team1Players")
                        .HasColumnType("text");

                    b.Property<string>("Team2")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Team2Kills")
                        .HasColumnType("integer");

                    b.Property<string>("Team2Picks")
                        .HasColumnType("text");

                    b.Property<string>("Team2Players")
                        .HasColumnType("text");

                    b.Property<string>("Tournament")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WinTeam")
                        .HasColumnType("text");

                    b.HasKey("LeaguepediaGameIdAndTitle");

                    b.ToTable("Import_ScoreboardGames");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Import_TeamsTableEntities.Import_TeamsTableEntity", b =>
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

                    b.ToTable("Import_TeamsTable");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Import_YoutubeDataEntities.Import_YoutubeDataEntity", b =>
                {
                    b.Property<string>("YoutubeVideoId")
                        .HasColumnType("text")
                        .HasComment("Can begin with uppercase letters, numbers, lowercase letters, - and _ , appending single quotation to handle this.");

                    b.Property<string>("LeaguepediaGameIdAndTitle")
                        .HasColumnType("text");

                    b.Property<string>("PlaylistId")
                        .HasColumnType("text")
                        .HasColumnName("PlaylistName");

                    b.Property<string>("PlaylistTitle")
                        .HasColumnType("text")
                        .HasColumnName("PlaylistTitle");

                    b.Property<DateTime>("PublishedAt_utc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ThumbnailUrl")
                        .HasMaxLength(2083)
                        .HasColumnType("character varying(2083)");

                    b.Property<string>("VideoTitle")
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

                    b.ToTable("Import_YoutubeData", (string)null);
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Processed_LeagueTeamEntities.Processed_LeagueTeamEntity", b =>
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

                    b.ToTable("Processed_LeagueTeams");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Processed_ProPlayerEntities.Processed_ProPlayerEntity", b =>
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

                    b.ToTable("Processed_ProPlayers");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Processed_TeamNameHistoryEntities.Processed_TeamNameHistoryEntity", b =>
                {
                    b.Property<string>("CurrentTeamName")
                        .HasColumnType("text");

                    b.Property<string>("NameHistory")
                        .HasColumnType("text");

                    b.HasKey("CurrentTeamName");

                    b.ToTable("Processed_TeamNameHistory");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Processed_TeamRenameEntities.Import_TeamRenameEntity", b =>
                {
                    b.Property<string>("OriginalName")
                        .HasColumnType("text")
                        .HasColumnOrder(0);

                    b.Property<string>("NewName")
                        .HasColumnType("text")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("ChangeDate_utc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(2);

                    b.Property<string>("IsSamePage")
                        .HasColumnType("text");

                    b.Property<string>("NewsId")
                        .HasColumnType("text");

                    b.Property<string>("Verb")
                        .HasColumnType("text");

                    b.HasKey("OriginalName", "NewName", "ChangeDate_utc");

                    b.ToTable("Processed_TeamRenames");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Processed_YoutubePlaylistEntities.Processed_YoutubePlaylistEntity", b =>
                {
                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<string>("PlaylistId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("name");

                    b.ToTable("Processed_YoutubePlaylists");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.YoutubeMatchExtractEntities.Processed_YoutubeDataEntity", b =>
                {
                    b.Property<string>("YoutubeVideoId")
                        .HasColumnType("text")
                        .HasComment("Can begin with uppercase letters, numbers, lowercase letters, - and _ , appending single quotation to handle this.");

                    b.Property<string>("GameDayIdentifier")
                        .HasColumnType("text");

                    b.Property<int?>("GameNumber")
                        .HasColumnType("integer");

                    b.Property<string>("GameWeekIdentifier")
                        .HasColumnType("text");

                    b.Property<bool>("IsSeries")
                        .HasColumnType("boolean");

                    b.Property<string>("PlayListId")
                        .HasColumnType("text");

                    b.Property<string>("PlayListTitle")
                        .HasColumnType("text");

                    b.Property<DateTime>("PublishedAt_utc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Season")
                        .HasColumnType("text");

                    b.Property<string>("Team1Long")
                        .HasColumnType("text");

                    b.Property<string>("Team1Short")
                        .HasColumnType("text");

                    b.Property<string>("Team2Long")
                        .HasColumnType("text");

                    b.Property<string>("Team2Short")
                        .HasColumnType("text");

                    b.Property<string>("Tournament")
                        .HasColumnType("text");

                    b.Property<string>("VideoTitle")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("YoutubeVideoId");

                    b.ToTable("Processed_YoutubeMatchExtracts");
                });

            modelBuilder.Entity("Import_ScoreboardGamesEntityProcessed_ProPlayerEntity", b =>
                {
                    b.HasOne("LolMatchFilterNew.Domain.Entities.Import_ScoreboardGamesEntities.Import_ScoreboardGamesEntity", null)
                        .WithMany()
                        .HasForeignKey("Import_ScoreboardGamesEntityLeaguepediaGameIdAndTitle")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LolMatchFilterNew.Domain.Entities.Processed_ProPlayerEntities.Processed_ProPlayerEntity", null)
                        .WithMany()
                        .HasForeignKey("Team1PlayersNavLeaguepediaPlayerAllName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Import_ScoreboardGamesEntityProcessed_ProPlayerEntity1", b =>
                {
                    b.HasOne("LolMatchFilterNew.Domain.Entities.Import_ScoreboardGamesEntities.Import_ScoreboardGamesEntity", null)
                        .WithMany()
                        .HasForeignKey("Import_ScoreboardGamesEntity1LeaguepediaGameIdAndTitle")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LolMatchFilterNew.Domain.Entities.Processed_ProPlayerEntities.Processed_ProPlayerEntity", null)
                        .WithMany()
                        .HasForeignKey("Team2PlayersNavLeaguepediaPlayerAllName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Import_ScoreboardGamesEntityProcessed_ProPlayerEntity2", b =>
                {
                    b.HasOne("LolMatchFilterNew.Domain.Entities.Import_ScoreboardGamesEntities.Import_ScoreboardGamesEntity", null)
                        .WithMany()
                        .HasForeignKey("MatchesLeaguepediaGameIdAndTitle")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LolMatchFilterNew.Domain.Entities.Processed_ProPlayerEntities.Processed_ProPlayerEntity", null)
                        .WithMany()
                        .HasForeignKey("PlayersLeaguepediaPlayerAllName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Import_YoutubeDataEntities.Import_YoutubeDataEntity", b =>
                {
                    b.HasOne("LolMatchFilterNew.Domain.Entities.Import_ScoreboardGamesEntities.Import_ScoreboardGamesEntity", "LeaguepediaMatch")
                        .WithOne("YoutubeVideo")
                        .HasForeignKey("LolMatchFilterNew.Domain.Entities.Import_YoutubeDataEntities.Import_YoutubeDataEntity", "LeaguepediaGameIdAndTitle");

                    b.Navigation("LeaguepediaMatch");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.YoutubeMatchExtractEntities.Processed_YoutubeDataEntity", b =>
                {
                    b.HasOne("LolMatchFilterNew.Domain.Entities.Import_YoutubeDataEntities.Import_YoutubeDataEntity", "YoutubeVideo")
                        .WithOne("MatchExtract")
                        .HasForeignKey("LolMatchFilterNew.Domain.Entities.YoutubeMatchExtractEntities.Processed_YoutubeDataEntity", "YoutubeVideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("YoutubeVideo");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Import_ScoreboardGamesEntities.Import_ScoreboardGamesEntity", b =>
                {
                    b.Navigation("YoutubeVideo");
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Import_YoutubeDataEntities.Import_YoutubeDataEntity", b =>
                {
                    b.Navigation("MatchExtract")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
