using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLeagueTeamEntityFormerPlayers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProPlayers_Teams_CurrentTeam",
                table: "ProPlayers");

            migrationBuilder.DropTable(
                name: "TeamFormerPlayers");

            migrationBuilder.DropIndex(
                name: "IX_ProPlayers_CurrentTeam",
                table: "ProPlayers");

            migrationBuilder.RenameColumn(
                name: "previousTeamNames",
                table: "Teams",
                newName: "PreviousTeamNames");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YoutubePlaylists");

            migrationBuilder.RenameColumn(
                name: "PreviousTeamNames",
                table: "Teams",
                newName: "previousTeamNames");

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
                name: "IX_ProPlayers_CurrentTeam",
                table: "ProPlayers",
                column: "CurrentTeam");

            migrationBuilder.CreateIndex(
                name: "IX_TeamFormerPlayers_PreviousTeamsTeamName",
                table: "TeamFormerPlayers",
                column: "PreviousTeamsTeamName");

            migrationBuilder.AddForeignKey(
                name: "FK_ProPlayers_Teams_CurrentTeam",
                table: "ProPlayers",
                column: "CurrentTeam",
                principalTable: "Teams",
                principalColumn: "TeamName",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
