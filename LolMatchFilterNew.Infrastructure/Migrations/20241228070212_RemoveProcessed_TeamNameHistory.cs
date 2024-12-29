using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProcessed_TeamNameHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Processed_TeamNameHistory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Processed_TeamNameHistory",
                columns: table => new
                {
                    CurrentTeamName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ChangeDepth = table.Column<int>(type: "integer", nullable: false),
                    ChangeType = table.Column<string>(type: "text", nullable: false),
                    ChangedTo = table.Column<string>(type: "text", nullable: false),
                    ParentOrganization = table.Column<string>(type: "text", nullable: false),
                    PreviousName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processed_TeamNameHistory", x => x.CurrentTeamName);
                });
        }
    }
}
