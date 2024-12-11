using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeYoutubeDataColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
            name: "LeaguepediaGameIdAndTitle",
            table: "Import_YoutubeData",
            newName: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
           name: "GameId",
           table: "Import_YoutubeData",
           newName: "LeaguepediaGameIdAndTitle");
        }
    }
}
