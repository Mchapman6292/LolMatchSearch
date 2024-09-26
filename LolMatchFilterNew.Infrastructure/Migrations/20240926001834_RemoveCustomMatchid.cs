using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCustomMatchid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomMatchId",
                table: "YoutubeVideoResults");

            migrationBuilder.DropColumn(
                name: "CustomMatchId",
                table: "LeaguepediaMatchDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomMatchId",
                table: "YoutubeVideoResults",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomMatchId",
                table: "LeaguepediaMatchDetails",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
