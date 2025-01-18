using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTournamentPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_import_tournament",
                table: "import_tournament");

            migrationBuilder.AlterColumn<string>(
                name: "overview_page",
                table: "import_tournament",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_import_tournament",
                table: "import_tournament",
                column: "overview_page");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_import_tournament",
                table: "import_tournament");

            migrationBuilder.AlterColumn<string>(
                name: "overview_page",
                table: "import_tournament",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_import_tournament",
                table: "import_tournament",
                column: "tournament_name");
        }
    }
}
