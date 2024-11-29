using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamRedirects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Import_TeamRedirect",
                columns: table => new
                {
                    PageName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    AllName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    OtherName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    UniqueLine = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Import_TeamRedirect", x => new { x.PageName, x.AllName });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Import_TeamRedirect");
        }
    }
}
