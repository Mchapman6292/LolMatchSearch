using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TeamNavigationOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaguepediaMatchDetails_Teams_Team1NavigationTeamName",
                table: "LeaguepediaMatchDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaguepediaMatchDetails_Teams_Team2NavigationTeamName",
                table: "LeaguepediaMatchDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_YoutubeVideoResults_LeaguepediaMatchDetails_LeaguepediaGame~",
                table: "YoutubeVideoResults");

            migrationBuilder.AlterColumn<string>(
                name: "LeaguepediaGameIdAndTitle",
                table: "YoutubeVideoResults",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CurrentTeam",
                table: "ProPlayers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Team2NavigationTeamName",
                table: "LeaguepediaMatchDetails",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Team1NavigationTeamName",
                table: "LeaguepediaMatchDetails",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaguepediaMatchDetails_Teams_Team1NavigationTeamName",
                table: "LeaguepediaMatchDetails",
                column: "Team1NavigationTeamName",
                principalTable: "Teams",
                principalColumn: "TeamName",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaguepediaMatchDetails_Teams_Team2NavigationTeamName",
                table: "LeaguepediaMatchDetails",
                column: "Team2NavigationTeamName",
                principalTable: "Teams",
                principalColumn: "TeamName",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_YoutubeVideoResults_LeaguepediaMatchDetails_LeaguepediaGame~",
                table: "YoutubeVideoResults",
                column: "LeaguepediaGameIdAndTitle",
                principalTable: "LeaguepediaMatchDetails",
                principalColumn: "LeaguepediaGameIdAndTitle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaguepediaMatchDetails_Teams_Team1NavigationTeamName",
                table: "LeaguepediaMatchDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaguepediaMatchDetails_Teams_Team2NavigationTeamName",
                table: "LeaguepediaMatchDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_YoutubeVideoResults_LeaguepediaMatchDetails_LeaguepediaGame~",
                table: "YoutubeVideoResults");

            migrationBuilder.AlterColumn<string>(
                name: "LeaguepediaGameIdAndTitle",
                table: "YoutubeVideoResults",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CurrentTeam",
                table: "ProPlayers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Team2NavigationTeamName",
                table: "LeaguepediaMatchDetails",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Team1NavigationTeamName",
                table: "LeaguepediaMatchDetails",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaguepediaMatchDetails_Teams_Team1NavigationTeamName",
                table: "LeaguepediaMatchDetails",
                column: "Team1NavigationTeamName",
                principalTable: "Teams",
                principalColumn: "TeamName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaguepediaMatchDetails_Teams_Team2NavigationTeamName",
                table: "LeaguepediaMatchDetails",
                column: "Team2NavigationTeamName",
                principalTable: "Teams",
                principalColumn: "TeamName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_YoutubeVideoResults_LeaguepediaMatchDetails_LeaguepediaGame~",
                table: "YoutubeVideoResults",
                column: "LeaguepediaGameIdAndTitle",
                principalTable: "LeaguepediaMatchDetails",
                principalColumn: "LeaguepediaGameIdAndTitle",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
