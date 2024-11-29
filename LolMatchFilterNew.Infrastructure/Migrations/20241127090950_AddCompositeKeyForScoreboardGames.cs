using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCompositeKeyForScoreboardGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Import_ScoreboardGames",
                table: "Import_ScoreboardGames");

            // Add this step to create the GameId column
            migrationBuilder.AddColumn<string>(
                name: "GameId",
                table: "Import_ScoreboardGames",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GameName",
                table: "Import_YoutubeData",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "GameName",
                table: "Import_ScoreboardGames",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Import_ScoreboardGames",
                table: "Import_ScoreboardGames",
                columns: new[] { "GameName", "GameId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Import_ScoreboardGames",
                table: "Import_ScoreboardGames");

            migrationBuilder.DropColumn(
                name: "GameName",
                table: "Import_YoutubeData");

            // Add this to remove the GameId column in rollback
            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Import_ScoreboardGames");

            migrationBuilder.AlterColumn<string>(
                name: "GameName",
                table: "Import_ScoreboardGames",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Import_ScoreboardGames",
                table: "Import_ScoreboardGames",
                column: "GameId");
        }
    }
}
