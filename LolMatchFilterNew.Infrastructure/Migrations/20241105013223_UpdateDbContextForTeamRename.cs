using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbContextForTeamRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamRenameEntity",
                table: "TeamRenameEntity");

            migrationBuilder.RenameTable(
                name: "TeamRenameEntity",
                newName: "TeamRenames");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamRenames",
                table: "TeamRenames",
                columns: new[] { "OriginalName", "NewName", "Date" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamRenames",
                table: "TeamRenames");

            migrationBuilder.RenameTable(
                name: "TeamRenames",
                newName: "TeamRenameEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamRenameEntity",
                table: "TeamRenameEntity",
                columns: new[] { "OriginalName", "NewName", "Date" });
        }
    }
}
