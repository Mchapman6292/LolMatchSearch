using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMigratonRemoveSlotPropertyFromRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slot",
                table: "TeamRenameEntity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slot",
                table: "TeamRenameEntity",
                type: "text",
                nullable: true);
        }
    }
}
