using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
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
                name: "Teams",
                columns: table => new
                {
                    TeamName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamName);
                });

            migrationBuilder.CreateTable(
                name: "YoutubeVideoResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    YoutubeVideoId = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    YoutubeResultHyperlink = table.Column<string>(type: "text", nullable: false),
                    LeaguepediaGameIdAndTitle = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YoutubeVideoResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YoutubeVideoResults_LeaguepediaMatchDetails_LeaguepediaGame~",
                        column: x => x.LeaguepediaGameIdAndTitle,
                        principalTable: "LeaguepediaMatchDetails",
                        principalColumn: "LeaguepediaGameIdAndTitle");
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
                    table.ForeignKey(
                        name: "FK_ProPlayers_Teams_CurrentTeam",
                        column: x => x.CurrentTeam,
                        principalTable: "Teams",
                        principalColumn: "TeamName",
                        onDelete: ReferentialAction.SetNull);
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
                    LeaguepediaMatchDetailEntityLeaguepediaGameIdAndTitle = table.Column<string>(type: "text", nullable: false),
                    Team1PlayersNavLeaguepediaPlayerAllName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeam1Players", x => new { x.LeaguepediaMatchDetailEntityLeaguepediaGameIdAndTitle, x.Team1PlayersNavLeaguepediaPlayerAllName });
                    table.ForeignKey(
                        name: "FK_MatchTeam1Players_LeaguepediaMatchDetails_LeaguepediaMatchD~",
                        column: x => x.LeaguepediaMatchDetailEntityLeaguepediaGameIdAndTitle,
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
                    LeaguepediaMatchDetailEntity1LeaguepediaGameIdAndTitle = table.Column<string>(type: "text", nullable: false),
                    Team2PlayersNavLeaguepediaPlayerAllName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeam2Players", x => new { x.LeaguepediaMatchDetailEntity1LeaguepediaGameIdAndTitle, x.Team2PlayersNavLeaguepediaPlayerAllName });
                    table.ForeignKey(
                        name: "FK_MatchTeam2Players_LeaguepediaMatchDetails_LeaguepediaMatchD~",
                        column: x => x.LeaguepediaMatchDetailEntity1LeaguepediaGameIdAndTitle,
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
                name: "TeamFormerPlayers",
                columns: table => new
                {
                    FormerPlayersLeaguepediaPlayerAllName = table.Column<string>(type: "text", nullable: false),
                    PreviousTeamsTeamName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamFormerPlayers", x => new { x.FormerPlayersLeaguepediaPlayerAllName, x.PreviousTeamsTeamName });
                    table.ForeignKey(
                        name: "FK_TeamFormerPlayers_ProPlayers_FormerPlayersLeaguepediaPlayer~",
                        column: x => x.FormerPlayersLeaguepediaPlayerAllName,
                        principalTable: "ProPlayers",
                        principalColumn: "LeaguepediaPlayerAllName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamFormerPlayers_Teams_PreviousTeamsTeamName",
                        column: x => x.PreviousTeamsTeamName,
                        principalTable: "Teams",
                        principalColumn: "TeamName",
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
                name: "IX_ProPlayers_CurrentTeam",
                table: "ProPlayers",
                column: "CurrentTeam");

            migrationBuilder.CreateIndex(
                name: "IX_TeamFormerPlayers_PreviousTeamsTeamName",
                table: "TeamFormerPlayers",
                column: "PreviousTeamsTeamName");

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
                name: "MatchPlayers");

            migrationBuilder.DropTable(
                name: "MatchTeam1Players");

            migrationBuilder.DropTable(
                name: "MatchTeam2Players");

            migrationBuilder.DropTable(
                name: "TeamFormerPlayers");

            migrationBuilder.DropTable(
                name: "YoutubeVideoResults");

            migrationBuilder.DropTable(
                name: "ProPlayers");

            migrationBuilder.DropTable(
                name: "LeaguepediaMatchDetails");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
