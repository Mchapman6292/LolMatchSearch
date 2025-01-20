using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTournamentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longname",
                table: "TeamnamesSet",
                newName: "LongName");

            migrationBuilder.AddColumn<string>(
                name: "Team1",
                table: "YoutubeSet",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Team2",
                table: "YoutubeSet",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Team1Team_Id",
                table: "WesternMatchesSet",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Team2Team_Id",
                table: "WesternMatchesSet",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamNameId",
                table: "TeamnamesSet",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "import_tournament",
                columns: table => new
                {
                    name = table.Column<string>(type: "text", nullable: false),
                    overview_page = table.Column<string>(type: "text", nullable: true),
                    date_start = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    date_start_fuzzy = table.Column<string>(type: "text", nullable: true),
                    league = table.Column<string>(type: "text", nullable: true),
                    region = table.Column<string>(type: "text", nullable: true),
                    country = table.Column<string>(type: "text", nullable: true),
                    closest_timezone = table.Column<string>(type: "text", nullable: true),
                    event_type = table.Column<string>(type: "text", nullable: true),
                    standard_name = table.Column<string>(type: "text", nullable: true),
                    split = table.Column<string>(type: "text", nullable: true),
                    split_number = table.Column<int>(type: "integer", nullable: true),
                    split_main_page = table.Column<string>(type: "text", nullable: true),
                    tournament_level = table.Column<string>(type: "text", nullable: true),
                    is_qualifier = table.Column<bool>(type: "boolean", nullable: true),
                    is_playoffs = table.Column<bool>(type: "boolean", nullable: true),
                    is_official = table.Column<bool>(type: "boolean", nullable: true),
                    year = table.Column<string>(type: "text", nullable: true),
                    alternative_names = table.Column<string>(type: "text", nullable: true),
                    tags = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_import_tournament", x => x.name);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "import_tournament");

            migrationBuilder.DropColumn(
                name: "Team1",
                table: "YoutubeSet");

            migrationBuilder.DropColumn(
                name: "Team2",
                table: "YoutubeSet");

            migrationBuilder.DropColumn(
                name: "Team1Team_Id",
                table: "WesternMatchesSet");

            migrationBuilder.DropColumn(
                name: "Team2Team_Id",
                table: "WesternMatchesSet");

            migrationBuilder.DropColumn(
                name: "TeamNameId",
                table: "TeamnamesSet");

            migrationBuilder.RenameColumn(
                name: "LongName",
                table: "TeamnamesSet",
                newName: "Longname");
        }
    }
}
