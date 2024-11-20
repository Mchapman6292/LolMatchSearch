using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamLongNamesToYoutubeExtract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Team2",
                table: "YoutubeMatchExtracts",
                newName: "Team2Short");

            migrationBuilder.RenameColumn(
                name: "Team1",
                table: "YoutubeMatchExtracts",
                newName: "Team2Long");

            migrationBuilder.AddColumn<string>(
                name: "Team1Long",
                table: "YoutubeMatchExtracts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Team1Short",
                table: "YoutubeMatchExtracts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Team1Long",
                table: "YoutubeMatchExtracts");

            migrationBuilder.DropColumn(
                name: "Team1Short",
                table: "YoutubeMatchExtracts");

            migrationBuilder.RenameColumn(
                name: "Team2Short",
                table: "YoutubeMatchExtracts",
                newName: "Team2");

            migrationBuilder.RenameColumn(
                name: "Team2Long",
                table: "YoutubeMatchExtracts",
                newName: "Team1");
        }
    }
}
