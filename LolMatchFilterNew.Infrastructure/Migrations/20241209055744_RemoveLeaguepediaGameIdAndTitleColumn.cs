using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLeaguepediaGameIdAndTitleColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
           name: "LeaguepediaGameIdAndTitle",
           table: "Import_ScoreboardGames");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
            name: "LeaguepediaGameIdAndTitle",
            table: "Import_ScoreboardGames",
            type: "text",
            nullable: true);
        }
    }
}
