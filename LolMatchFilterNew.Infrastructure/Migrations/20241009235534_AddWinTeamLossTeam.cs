using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWinTeamLossTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Winner",
                table: "LeaguepediaMatchDetails");

            migrationBuilder.AddColumn<string>(
                name: "LossTeam",
                table: "LeaguepediaMatchDetails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WinTeam",
                table: "LeaguepediaMatchDetails",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LossTeam",
                table: "LeaguepediaMatchDetails");

            migrationBuilder.DropColumn(
                name: "WinTeam",
                table: "LeaguepediaMatchDetails");

            migrationBuilder.AddColumn<int>(
                name: "Winner",
                table: "LeaguepediaMatchDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
