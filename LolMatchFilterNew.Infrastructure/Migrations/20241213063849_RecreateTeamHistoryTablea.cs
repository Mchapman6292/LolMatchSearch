using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RecreateTeamHistoryTablea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop existing table
            migrationBuilder.DropTable(
                name: "Processed_TeamNameHistory");

            // Create new table with correct column types
            migrationBuilder.CreateTable(
                name: "Processed_TeamNameHistory",
                columns: table => new
                {
                    CurrentTeamName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    NameHistory = table.Column<string[]>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processed_TeamNameHistory", x => x.CurrentTeamName);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Processed_TeamNameHistory");
        }
    }
}
