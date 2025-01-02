﻿// <auto-generated />
using System;
using System.Collections.Generic;
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

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_ScoreboardGamesEntities.Import_ScoreboardGamesEntity", b =>
                {
                    b.Property<string>("GameName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("game_name");

                    b.Property<string>("GameId")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("game_id");

                    b.Property<DateTime?>("DateTime_utc")
                        .HasMaxLength(100)
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("datetime_utc");

                    b.Property<string>("LossTeam")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("loss_team");

                    b.Property<string>("MatchId")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("match_id");

                    b.Property<string>("Team1")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("team1");

                    b.Property<int?>("Team1Kills")
                        .HasColumnType("integer");

                    b.Property<string>("Team1Picks")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("team1_picks");

                    b.Property<string>("Team1Players")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("team1_players");

                    b.Property<string>("Team2")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("team2");

                    b.Property<int?>("Team2Kills")
                        .HasColumnType("integer");

                    b.Property<string>("Team2Picks")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("team2_picks");

                    b.Property<string>("Team2Players")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("team2_players");

                    b.Property<string>("Tournament")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("tournament");

                    b.Property<string>("WinTeam")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("win_team");

                    b.HasKey("GameName", "GameId");

                    b.ToTable("import_scoreboardgames", (string)null);
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRedirectEntities.Import_TeamRedirectEntity", b =>
                {
                    b.Property<string>("PageName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("page_name");

                    b.Property<string>("AllName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("all_name");

                    b.Property<string>("OtherName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("other_name");

                    b.Property<string>("UniqueLine")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("unique_line");

                    b.HasKey("PageName", "AllName");

                    b.ToTable("import_teamredirect", (string)null);
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRenameEntities.Import_TeamRenameEntity", b =>
                {
                    b.Property<string>("OriginalName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("original_name");

                    b.Property<string>("NewName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("new_name");

                    b.Property<DateTime?>("Date")
                        .HasMaxLength(10)
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<string>("IsSamePage")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("is_same_page");

                    b.Property<string>("NewsId")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("news_id");

                    b.Property<string>("Verb")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("verb");

                    b.HasKey("OriginalName", "NewName", "Date");

                    b.ToTable("import_teamrenames", (string)null);
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_Teamnames.Import_TeamnameEntity", b =>
                {
                    b.Property<string>("TeamnameId")
                        .HasColumnType("text")
                        .HasColumnName("teamname_id");

                    b.Property<List<string>>("Inputs")
                        .HasMaxLength(1000)
                        .HasColumnType("text[]")
                        .HasColumnName("inputs");

                    b.Property<string>("Longname")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("longname");

                    b.Property<string>("Medium")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("medium");

                    b.Property<string>("Short")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("short");

                    b.HasKey("TeamnameId");

                    b.ToTable("import_teamname", (string)null);
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamsTableEntities.Import_TeamsTableEntity", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<string>("Discord")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("discord");

                    b.Property<string>("Facebook")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("facebook");

                    b.Property<string>("Image")
                        .HasMaxLength(2083)
                        .HasColumnType("character varying(2083)")
                        .HasColumnName("image");

                    b.Property<string>("Instagram")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("instagram");

                    b.Property<bool>("IsDisbanded")
                        .HasColumnType("boolean")
                        .HasColumnName("is_disbanded");

                    b.Property<bool>("IsLowercase")
                        .HasColumnType("boolean")
                        .HasColumnName("is_lowercase");

                    b.Property<string>("Location")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("location");

                    b.Property<string>("OrganizationPage")
                        .HasMaxLength(2083)
                        .HasColumnType("character varying(2083)")
                        .HasColumnName("organization_page");

                    b.Property<string>("OverviewPage")
                        .HasMaxLength(2083)
                        .HasColumnType("character varying(2083)")
                        .HasColumnName("overview_page");

                    b.Property<string>("Region")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("region");

                    b.Property<string>("RenamedTo")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("renamed_to");

                    b.Property<string>("RosterPhoto")
                        .HasMaxLength(2083)
                        .HasColumnType("character varying(2083)")
                        .HasColumnName("roster_photo");

                    b.Property<string>("Short")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("short");

                    b.Property<string>("Snapchat")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("snapchat");

                    b.Property<string>("Subreddit")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("subreddit");

                    b.Property<string>("TeamLocation")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("team_location");

                    b.Property<string>("Twitter")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("twitter");

                    b.Property<string>("Vk")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("vk");

                    b.Property<string>("Website")
                        .HasColumnType("text")
                        .HasColumnName("website");

                    b.Property<string>("Youtube")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("youtube");

                    b.HasKey("Name");

                    b.ToTable("import_teamstable", (string)null);
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities.Import_YoutubeDataEntity", b =>
                {
                    b.Property<string>("YoutubeVideoId")
                        .HasColumnType("text")
                        .HasColumnName("youtube_video_id")
                        .HasComment("Can begin with uppercase letters, numbers, lowercase letters, - and _ , appending single quotation to handle this.");

                    b.Property<string>("PlaylistId")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("playlist_id");

                    b.Property<string>("PlaylistTitle")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("playlist_title");

                    b.Property<DateTime?>("PublishedAt_utc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("published_at_utc");

                    b.Property<string>("ThumbnailUrl")
                        .HasMaxLength(2083)
                        .HasColumnType("character varying(2083)")
                        .HasColumnName("thumbnail_url");

                    b.Property<string>("VideoTitle")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("video_title");

                    b.Property<string>("YoutubeResultHyperlink")
                        .HasMaxLength(2083)
                        .HasColumnType("character varying(2083)")
                        .HasColumnName("youtube_result_hyperlink");

                    b.HasKey("YoutubeVideoId");

                    b.ToTable("import_youtubedata", (string)null);
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_LeagueTeamEntities.Processed_LeagueTeamEntity", b =>
                {
                    b.Property<string>("TeamName")
                        .HasColumnType("text")
                        .HasColumnName("team_name");

                    b.Property<string>("NameShort")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name_short");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("region");

                    b.HasKey("TeamName");

                    b.ToTable("processed_leagueteam", (string)null);
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_ProPlayerEntities.Processed_ProPlayerEntity", b =>
                {
                    b.Property<string>("LeaguepediaPlayerAllName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("leaguepedia_player_all_name");

                    b.Property<string>("CurrentTeam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("current_team");

                    b.Property<string>("InGameName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("in_game_name");

                    b.Property<string>("LeaguepediaPlayerId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("leaguepedia_player_id");

                    b.Property<string>("PreviousInGameNames")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("previous_in_game_names");

                    b.Property<string>("RealName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("real_name");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("role");

                    b.HasKey("LeaguepediaPlayerAllName");

                    b.ToTable("processed_proplayers", (string)null);
                });

            modelBuilder.Entity("LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_YoutubeDataEntities.Processed_YoutubeDataEntity", b =>
                {
                    b.Property<string>("YoutubeVideoId")
                        .HasColumnType("text")
                        .HasColumnName("youtube_video_id")
                        .HasComment("Can begin with uppercase letters, numbers, lowercase letters, - and _ , appending single quotation to handle this.");

                    b.Property<string>("GameDayIdentifier")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("game_day_identifier");

                    b.Property<int?>("GameNumber")
                        .HasColumnType("integer")
                        .HasColumnName("game_number");

                    b.Property<string>("GameWeekIdentifier")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("game_week_identifier");

                    b.Property<bool>("IsSeries")
                        .HasColumnType("boolean")
                        .HasColumnName("is_series");

                    b.Property<string>("PlayListId")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("playlist_id");

                    b.Property<string>("PlayListTitle")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("playlist_title");

                    b.Property<DateTime>("PublishedAt_utc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("published_at_utc");

                    b.Property<string>("Season")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("season");

                    b.Property<string>("Team1Long")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("team1_long");

                    b.Property<string>("Team1Short")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("team1_short");

                    b.Property<string>("Team2Long")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("team2_long");

                    b.Property<string>("Team2Short")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("team2_short");

                    b.Property<string>("Tournament")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("tournament");

                    b.Property<string>("VideoTitle")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("video_title");

                    b.HasKey("YoutubeVideoId");

                    b.ToTable("processed_youtubedata", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
