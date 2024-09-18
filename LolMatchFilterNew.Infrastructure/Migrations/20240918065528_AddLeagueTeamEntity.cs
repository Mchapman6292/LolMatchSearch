using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLeagueTeamEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "ProPlayers",
                columns: table => new
                {
                    LeaguepediaPlayerAllName = table.Column<string>(type: "text", nullable: false),
                    LeaguepediaPlayerId = table.Column<string>(type: "text", nullable: false),
                    CurrentTeam = table.Column<string>(type: "text", nullable: false),
                    InGameName = table.Column<string>(type: "text", nullable: false),
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
                name: "LeaguepediaMatchDetails",
                columns: table => new
                {
                    LeaguepediaGameIdAndTitle = table.Column<string>(type: "text", nullable: false),
                    CustomMatchId = table.Column<string>(type: "text", nullable: false),
                    DateTimeUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Tournament = table.Column<string>(type: "text", nullable: false),
                    Team1 = table.Column<string>(type: "text", nullable: false),
                    Team2 = table.Column<string>(type: "text", nullable: false),
                    Team1Picks = table.Column<List<string>>(type: "text[]", nullable: false),
                    Team2Picks = table.Column<List<string>>(type: "text[]", nullable: false),
                    Team1Side = table.Column<int>(type: "integer", nullable: false),
                    Winner = table.Column<string>(type: "text", nullable: false),
                    Team1NavigationTeamName = table.Column<string>(type: "text", nullable: false),
                    Team2NavigationTeamName = table.Column<string>(type: "text", nullable: false),
                    ProPlayerEntityLeaguepediaPlayerAllName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaguepediaMatchDetails", x => x.LeaguepediaGameIdAndTitle);
                    table.ForeignKey(
                        name: "FK_LeaguepediaMatchDetails_ProPlayers_ProPlayerEntityLeagueped~",
                        column: x => x.ProPlayerEntityLeaguepediaPlayerAllName,
                        principalTable: "ProPlayers",
                        principalColumn: "LeaguepediaPlayerAllName");
                    table.ForeignKey(
                        name: "FK_LeaguepediaMatchDetails_Teams_Team1NavigationTeamName",
                        column: x => x.Team1NavigationTeamName,
                        principalTable: "Teams",
                        principalColumn: "TeamName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaguepediaMatchDetails_Teams_Team2NavigationTeamName",
                        column: x => x.Team2NavigationTeamName,
                        principalTable: "Teams",
                        principalColumn: "TeamName",
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

            migrationBuilder.CreateTable(
                name: "YoutubeVideoResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomMatchId = table.Column<string>(type: "text", nullable: false),
                    YoutubeVideoId = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    YoutubeResultHyperlink = table.Column<string>(type: "text", nullable: false),
                    LeaguepediaGameIdAndTitle = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YoutubeVideoResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YoutubeVideoResults_LeaguepediaMatchDetails_LeaguepediaGame~",
                        column: x => x.LeaguepediaGameIdAndTitle,
                        principalTable: "LeaguepediaMatchDetails",
                        principalColumn: "LeaguepediaGameIdAndTitle",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaguepediaMatchDetails_ProPlayerEntityLeaguepediaPlayerAll~",
                table: "LeaguepediaMatchDetails",
                column: "ProPlayerEntityLeaguepediaPlayerAllName");

            migrationBuilder.CreateIndex(
                name: "IX_LeaguepediaMatchDetails_Team1NavigationTeamName",
                table: "LeaguepediaMatchDetails",
                column: "Team1NavigationTeamName");

            migrationBuilder.CreateIndex(
                name: "IX_LeaguepediaMatchDetails_Team2NavigationTeamName",
                table: "LeaguepediaMatchDetails",
                column: "Team2NavigationTeamName");

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
                name: "TeamFormerPlayers");

            migrationBuilder.DropTable(
                name: "YoutubeVideoResults");

            migrationBuilder.DropTable(
                name: "LeaguepediaMatchDetails");

            migrationBuilder.DropTable(
                name: "ProPlayers");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
