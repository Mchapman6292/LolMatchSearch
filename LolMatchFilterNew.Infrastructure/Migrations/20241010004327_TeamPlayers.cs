using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TeamPlayers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameName",
                table: "LeaguepediaMatchDetails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Team1Kills",
                table: "LeaguepediaMatchDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<List<string>>(
                name: "Team1Players",
                table: "LeaguepediaMatchDetails",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "Team2Kills",
                table: "LeaguepediaMatchDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<List<string>>(
                name: "Team2Players",
                table: "LeaguepediaMatchDetails",
                type: "text[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameName",
                table: "LeaguepediaMatchDetails");

            migrationBuilder.DropColumn(
                name: "Team1Kills",
                table: "LeaguepediaMatchDetails");

            migrationBuilder.DropColumn(
                name: "Team1Players",
                table: "LeaguepediaMatchDetails");

            migrationBuilder.DropColumn(
                name: "Team2Kills",
                table: "LeaguepediaMatchDetails");

            migrationBuilder.DropColumn(
                name: "Team2Players",
                table: "LeaguepediaMatchDetails");
        }
    }
}
