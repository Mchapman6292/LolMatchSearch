using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveWinnerPropertFromLeaguepediaEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Team1Side",
                table: "LeaguepediaMatchDetails");

            migrationBuilder.DropColumn(
                name: "Winner",
                table: "LeaguepediaMatchDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Team1Side",
                table: "LeaguepediaMatchDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Winner",
                table: "LeaguepediaMatchDetails",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
