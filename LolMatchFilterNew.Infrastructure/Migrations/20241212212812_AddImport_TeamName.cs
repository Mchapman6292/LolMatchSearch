using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddImport_TeamName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Import_Teamname",
                columns: table => new
                {
                    TeamnameId = table.Column<string>(type: "text", nullable: false),
                    Longname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Short = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Medium = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Inputs = table.Column<List<string>>(type: "text[]", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Import_Teamname", x => x.TeamnameId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Import_Teamname");
        }
    }
}
