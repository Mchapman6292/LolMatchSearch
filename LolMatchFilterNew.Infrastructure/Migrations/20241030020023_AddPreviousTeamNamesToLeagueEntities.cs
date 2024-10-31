using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPreviousTeamNamesToLeagueEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "previousTeamNames",
                table: "Teams",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "previousTeamNames",
                table: "Teams");
        }
    }
}
