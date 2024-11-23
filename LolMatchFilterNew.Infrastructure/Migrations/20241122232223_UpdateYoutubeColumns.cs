using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateYoutubeColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlaylistId",
                table: "YoutubeVideoResults",
                newName: "PlaylistName");

            migrationBuilder.AddColumn<string>(
                name: "PlaylistTitle",
                table: "YoutubeVideoResults",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlayListTitle",
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
                name: "PlayListTitle",
                table: "YoutubeMatchExtracts");

            migrationBuilder.RenameColumn(
                name: "PlaylistName",
                table: "YoutubeVideoResults",
                newName: "PlaylistId");
        }
    }
}
