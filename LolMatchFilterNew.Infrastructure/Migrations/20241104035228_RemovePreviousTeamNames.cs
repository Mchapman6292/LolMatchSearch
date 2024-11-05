using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovePreviousTeamNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviousTeamNames",
                table: "Teams");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreviousTeamNames",
                table: "Teams",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
