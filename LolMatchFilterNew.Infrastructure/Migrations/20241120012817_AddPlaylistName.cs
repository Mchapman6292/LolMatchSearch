using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPlaylistName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlayListTitile",
                table: "YoutubeMatchExtracts",
                newName: "PlayListTitle");

            migrationBuilder.AddColumn<string>(
                name: "PlaylistTitle",
                table: "YoutubeVideoResults",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlayListName",
                table: "YoutubeMatchExtracts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaylistTitle",
                table: "YoutubeVideoResults");

            migrationBuilder.DropColumn(
                name: "PlayListName",
                table: "YoutubeMatchExtracts");

            migrationBuilder.RenameColumn(
                name: "PlayListTitle",
                table: "YoutubeMatchExtracts",
                newName: "PlayListTitile");
        }
    }
}
