using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Import_ScoreboardGames",
                columns: table => new
                {
                    LeaguepediaGameIdAndTitle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    GameName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    League = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DateTime_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Tournament = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Team1 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Team2 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Team1Players = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Team2Players = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Team1Picks = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Team2Picks = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    WinTeam = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LossTeam = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Team1Kills = table.Column<int>(type: "integer", nullable: true),
                    Team2Kills = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Import_ScoreboardGames", x => x.LeaguepediaGameIdAndTitle);
                });

            migrationBuilder.CreateTable(
                name: "Import_TeamRenames",
                columns: table => new
                {
                    OriginalName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    NewName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Verb = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsSamePage = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    NewsId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Import_TeamRenames", x => new { x.OriginalName, x.NewName, x.Date });
                });

            migrationBuilder.CreateTable(
                name: "Import_TeamsTable",
                columns: table => new
                {
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    OverviewPage = table.Column<string>(type: "character varying(2083)", maxLength: 2083, nullable: true),
                    Short = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Location = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TeamLocation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Region = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    OrganizationPage = table.Column<string>(type: "character varying(2083)", maxLength: 2083, nullable: true),
                    Image = table.Column<string>(type: "character varying(2083)", maxLength: 2083, nullable: true),
                    Twitter = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Youtube = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Facebook = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Instagram = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Discord = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Snapchat = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Vk = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Subreddit = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Website = table.Column<string>(type: "text", nullable: true),
                    RosterPhoto = table.Column<string>(type: "character varying(2083)", maxLength: 2083, nullable: true),
                    IsDisbanded = table.Column<bool>(type: "boolean", nullable: false),
                    RenamedTo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsLowercase = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Import_TeamsTable", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Import_YoutubeData",
                columns: table => new
                {
                    YoutubeVideoId = table.Column<string>(type: "text", nullable: false, comment: "Can begin with uppercase letters, numbers, lowercase letters, - and _ , appending single quotation to handle this."),
                    VideoTitle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PlaylistId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PlaylistTitle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PublishedAt_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    YoutubeResultHyperlink = table.Column<string>(type: "character varying(2083)", maxLength: 2083, nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "character varying(2083)", maxLength: 2083, nullable: false),
                    LeaguepediaGameIdAndTitle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Import_YoutubeData", x => x.YoutubeVideoId);
                });

            migrationBuilder.CreateTable(
                name: "Processed_LeagueTeamEntity",
                columns: table => new
                {
                    TeamName = table.Column<string>(type: "text", nullable: false),
                    NameShort = table.Column<string>(type: "text", nullable: false),
                    Region = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processed_LeagueTeamEntity", x => x.TeamName);
                });

            migrationBuilder.CreateTable(
                name: "Processed_ProPlayers",
                columns: table => new
                {
                    LeaguepediaPlayerAllName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LeaguepediaPlayerId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CurrentTeam = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    InGameName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PreviousInGameNames = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    RealName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processed_ProPlayers", x => x.LeaguepediaPlayerAllName);
                });

            migrationBuilder.CreateTable(
                name: "Processed_TeamNameHistory",
                columns: table => new
                {
                    CurrentTeamName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    NameHistory = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processed_TeamNameHistory", x => x.CurrentTeamName);
                });

            migrationBuilder.CreateTable(
                name: "Processed_YoutubeData",
                columns: table => new
                {
                    YoutubeVideoId = table.Column<string>(type: "text", nullable: false, comment: "Can begin with uppercase letters, numbers, lowercase letters, - and _ , appending single quotation to handle this."),
                    VideoTitle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PlayListId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PlayListTitle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PublishedAt_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Team1Short = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Team1Long = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Team2Short = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Team2Long = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Tournament = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    GameWeekIdentifier = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    GameDayIdentifier = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Season = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsSeries = table.Column<bool>(type: "boolean", nullable: false),
                    GameNumber = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processed_YoutubeData", x => x.YoutubeVideoId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Import_ScoreboardGames");

            migrationBuilder.DropTable(
                name: "Import_TeamRenames");

            migrationBuilder.DropTable(
                name: "Import_TeamsTable");

            migrationBuilder.DropTable(
                name: "Import_YoutubeData");

            migrationBuilder.DropTable(
                name: "Processed_LeagueTeamEntity");

            migrationBuilder.DropTable(
                name: "Processed_ProPlayers");

            migrationBuilder.DropTable(
                name: "Processed_TeamNameHistory");

            migrationBuilder.DropTable(
                name: "Processed_YoutubeData");
        }
    }
}
