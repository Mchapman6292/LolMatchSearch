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
                name: "LeaguepediaMatchDetails",
                columns: table => new
                {
                    LeaguepediaGameIdAndTitle = table.Column<string>(type: "text", nullable: false),
                    GameName = table.Column<string>(type: "text", nullable: false),
                    League = table.Column<string>(type: "text", nullable: false),
                    DateTimeUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Tournament = table.Column<string>(type: "text", nullable: false),
                    Team1 = table.Column<string>(type: "text", nullable: false),
                    Team2 = table.Column<string>(type: "text", nullable: false),
                    Team1Players = table.Column<string>(type: "text", nullable: false),
                    Team2Players = table.Column<string>(type: "text", nullable: false),
                    Team1Picks = table.Column<string>(type: "text", nullable: false),
                    Team2Picks = table.Column<string>(type: "text", nullable: false),
                    WinTeam = table.Column<string>(type: "text", nullable: false),
                    LossTeam = table.Column<string>(type: "text", nullable: false),
                    Team1Kills = table.Column<int>(type: "integer", nullable: false),
                    Team2Kills = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaguepediaMatchDetails", x => x.LeaguepediaGameIdAndTitle);
                });

            migrationBuilder.CreateTable(
                name: "LOLTeams",
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
                    table.PrimaryKey("PK_LOLTeams", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "ProPlayers",
                columns: table => new
                {
                    LeaguepediaPlayerAllName = table.Column<string>(type: "text", nullable: false),
                    LeaguepediaPlayerId = table.Column<string>(type: "text", nullable: false),
                    CurrentTeam = table.Column<string>(type: "text", nullable: true),
                    InGameName = table.Column<string>(type: "text", nullable: false),
                    PreviousInGameNames = table.Column<string>(type: "text", nullable: false),
                    RealName = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProPlayers", x => x.LeaguepediaPlayerAllName);
                });

            migrationBuilder.CreateTable(
                name: "TeamNameHistory",
                columns: table => new
                {
                    CurrentTeamName = table.Column<string>(type: "text", nullable: false),
                    NameHistory = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamNameHistory", x => x.CurrentTeamName);
                });

            migrationBuilder.CreateTable(
                name: "TeamRenames",
                columns: table => new
                {
                    OriginalName = table.Column<string>(type: "text", nullable: false),
                    NewName = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Verb = table.Column<string>(type: "text", nullable: true),
                    IsSamePage = table.Column<string>(type: "text", nullable: true),
                    NewsId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamRenames", x => new { x.OriginalName, x.NewName, x.Date });
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamName = table.Column<string>(type: "text", nullable: false),
                    NameShort = table.Column<string>(type: "text", nullable: false),
                    Region = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamName);
                });

            migrationBuilder.CreateTable(
                name: "YoutubePlaylists",
                columns: table => new
                {
                    name = table.Column<string>(type: "text", nullable: false),
                    PlaylistId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YoutubePlaylists", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "YoutubeVideoResults",
                columns: table => new
                {
                    YoutubeVideoId = table.Column<string>(type: "text", nullable: false, comment: "Can begin with uppercase letters, numbers, lowercase letters, - and _ , appending single quotation to handle this."),
                    VideoTitle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PlaylistName = table.Column<string>(type: "text", nullable: true),
                    PlaylistTitle = table.Column<string>(type: "text", nullable: true),
                    PublishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    YoutubeResultHyperlink = table.Column<string>(type: "character varying(2083)", maxLength: 2083, nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "character varying(2083)", maxLength: 2083, nullable: true),
                    LeaguepediaGameIdAndTitle = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YoutubeVideoResults", x => x.YoutubeVideoId);
                    table.ForeignKey(
                        name: "FK_YoutubeVideoResults_LeaguepediaMatchDetails_LeaguepediaGame~",
                        column: x => x.LeaguepediaGameIdAndTitle,
                        principalTable: "LeaguepediaMatchDetails",
                        principalColumn: "LeaguepediaGameIdAndTitle");
                });

            migrationBuilder.CreateTable(
                name: "MatchPlayers",
                columns: table => new
                {
                    MatchesLeaguepediaGameIdAndTitle = table.Column<string>(type: "text", nullable: false),
                    PlayersLeaguepediaPlayerAllName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchPlayers", x => new { x.MatchesLeaguepediaGameIdAndTitle, x.PlayersLeaguepediaPlayerAllName });
                    table.ForeignKey(
                        name: "FK_MatchPlayers_LeaguepediaMatchDetails_MatchesLeaguepediaGame~",
                        column: x => x.MatchesLeaguepediaGameIdAndTitle,
                        principalTable: "LeaguepediaMatchDetails",
                        principalColumn: "LeaguepediaGameIdAndTitle",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchPlayers_ProPlayers_PlayersLeaguepediaPlayerAllName",
                        column: x => x.PlayersLeaguepediaPlayerAllName,
                        principalTable: "ProPlayers",
                        principalColumn: "LeaguepediaPlayerAllName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeam1Players",
                columns: table => new
                {
                    Import_ScoreboardGamesEntityLeaguepediaGameIdAndTitle = table.Column<string>(type: "text", nullable: false),
                    Team1PlayersNavLeaguepediaPlayerAllName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeam1Players", x => new { x.Import_ScoreboardGamesEntityLeaguepediaGameIdAndTitle, x.Team1PlayersNavLeaguepediaPlayerAllName });
                    table.ForeignKey(
                        name: "FK_MatchTeam1Players_LeaguepediaMatchDetails_Import_Scoreboard~",
                        column: x => x.Import_ScoreboardGamesEntityLeaguepediaGameIdAndTitle,
                        principalTable: "LeaguepediaMatchDetails",
                        principalColumn: "LeaguepediaGameIdAndTitle",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchTeam1Players_ProPlayers_Team1PlayersNavLeaguepediaPlay~",
                        column: x => x.Team1PlayersNavLeaguepediaPlayerAllName,
                        principalTable: "ProPlayers",
                        principalColumn: "LeaguepediaPlayerAllName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeam2Players",
                columns: table => new
                {
                    Import_ScoreboardGamesEntity1LeaguepediaGameIdAndTitle = table.Column<string>(type: "text", nullable: false),
                    Team2PlayersNavLeaguepediaPlayerAllName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeam2Players", x => new { x.Import_ScoreboardGamesEntity1LeaguepediaGameIdAndTitle, x.Team2PlayersNavLeaguepediaPlayerAllName });
                    table.ForeignKey(
                        name: "FK_MatchTeam2Players_LeaguepediaMatchDetails_Import_Scoreboard~",
                        column: x => x.Import_ScoreboardGamesEntity1LeaguepediaGameIdAndTitle,
                        principalTable: "LeaguepediaMatchDetails",
                        principalColumn: "LeaguepediaGameIdAndTitle",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchTeam2Players_ProPlayers_Team2PlayersNavLeaguepediaPlay~",
                        column: x => x.Team2PlayersNavLeaguepediaPlayerAllName,
                        principalTable: "ProPlayers",
                        principalColumn: "LeaguepediaPlayerAllName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "YoutubeMatchExtracts",
                columns: table => new
                {
                    YoutubeVideoId = table.Column<string>(type: "text", nullable: false, comment: "Can begin with uppercase letters, numbers, lowercase letters, - and _ , appending single quotation to handle this."),
                    VideoTitle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PlayListId = table.Column<string>(type: "text", nullable: true),
                    PlayListTitle = table.Column<string>(type: "text", nullable: true),
                    PublishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Team1Short = table.Column<string>(type: "text", nullable: true),
                    Team1Long = table.Column<string>(type: "text", nullable: true),
                    Team2Short = table.Column<string>(type: "text", nullable: true),
                    Team2Long = table.Column<string>(type: "text", nullable: true),
                    Tournament = table.Column<string>(type: "text", nullable: true),
                    GameWeekIdentifier = table.Column<string>(type: "text", nullable: true),
                    GameDayIdentifier = table.Column<string>(type: "text", nullable: true),
                    Season = table.Column<string>(type: "text", nullable: true),
                    IsSeries = table.Column<bool>(type: "boolean", nullable: false),
                    GameNumber = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YoutubeMatchExtracts", x => x.YoutubeVideoId);
                    table.ForeignKey(
                        name: "FK_YoutubeMatchExtracts_YoutubeVideoResults_YoutubeVideoId",
                        column: x => x.YoutubeVideoId,
                        principalTable: "YoutubeVideoResults",
                        principalColumn: "YoutubeVideoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayers_PlayersLeaguepediaPlayerAllName",
                table: "MatchPlayers",
                column: "PlayersLeaguepediaPlayerAllName");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeam1Players_Team1PlayersNavLeaguepediaPlayerAllName",
                table: "MatchTeam1Players",
                column: "Team1PlayersNavLeaguepediaPlayerAllName");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeam2Players_Team2PlayersNavLeaguepediaPlayerAllName",
                table: "MatchTeam2Players",
                column: "Team2PlayersNavLeaguepediaPlayerAllName");

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeVideoResults_LeaguepediaGameIdAndTitle",
                table: "YoutubeVideoResults",
                column: "LeaguepediaGameIdAndTitle",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LOLTeams");

            migrationBuilder.DropTable(
                name: "MatchPlayers");

            migrationBuilder.DropTable(
                name: "MatchTeam1Players");

            migrationBuilder.DropTable(
                name: "MatchTeam2Players");

            migrationBuilder.DropTable(
                name: "TeamNameHistory");

            migrationBuilder.DropTable(
                name: "TeamRenames");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "YoutubeMatchExtracts");

            migrationBuilder.DropTable(
                name: "YoutubePlaylists");

            migrationBuilder.DropTable(
                name: "ProPlayers");

            migrationBuilder.DropTable(
                name: "YoutubeVideoResults");

            migrationBuilder.DropTable(
                name: "LeaguepediaMatchDetails");
        }
    }
}
