using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CheckUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamnamesSet",
                columns: table => new
                {
                    Longname = table.Column<string>(type: "text", nullable: true),
                    Medium = table.Column<string>(type: "text", nullable: true),
                    Short = table.Column<string>(type: "text", nullable: true),
                    FormattedInputs = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "WesternMatchesSet",
                columns: table => new
                {
                    Game_Id = table.Column<string>(type: "text", nullable: false),
                    Match_Id = table.Column<string>(type: "text", nullable: false),
                    DateTime_Utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Tournament = table.Column<string>(type: "text", nullable: true),
                    Team1 = table.Column<string>(type: "text", nullable: true),
                    Team1_Players = table.Column<string>(type: "text", nullable: true),
                    Team1_Picks = table.Column<string>(type: "text", nullable: true),
                    Team2 = table.Column<string>(type: "text", nullable: true),
                    Team2_Players = table.Column<string>(type: "text", nullable: true),
                    Team2_Picks = table.Column<string>(type: "text", nullable: true),
                    Win_Team = table.Column<string>(type: "text", nullable: true),
                    Loss_Team = table.Column<string>(type: "text", nullable: true),
                    Team1_Region = table.Column<string>(type: "text", nullable: true),
                    Team1_Longname = table.Column<string>(type: "text", nullable: true),
                    Team1_Medium = table.Column<string>(type: "text", nullable: true),
                    Team1_Short = table.Column<string>(type: "text", nullable: true),
                    Team1_Inputs = table.Column<List<string>>(type: "text[]", nullable: true),
                    Team2_Region = table.Column<string>(type: "text", nullable: true),
                    Team2_Longname = table.Column<string>(type: "text", nullable: true),
                    Team2_Medium = table.Column<string>(type: "text", nullable: true),
                    Team2_Short = table.Column<string>(type: "text", nullable: true),
                    Team2_Inputs = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "YoutubeSet",
                columns: table => new
                {
                    YoutubeVideoId = table.Column<string>(type: "text", nullable: false),
                    VideoTitle = table.Column<string>(type: "text", nullable: false),
                    PlaylistId = table.Column<string>(type: "text", nullable: true),
                    PlaylistTitle = table.Column<string>(type: "text", nullable: true),
                    PublishedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    YoutubeResultHyperlink = table.Column<string>(type: "text", nullable: true),
                    ThumbnailUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamnamesSet");

            migrationBuilder.DropTable(
                name: "WesternMatchesSet");

            migrationBuilder.DropTable(
                name: "YoutubeSet");
        }
    }
}
