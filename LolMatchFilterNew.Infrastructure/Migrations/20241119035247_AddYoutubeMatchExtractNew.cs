using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddYoutubeMatchExtractNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentNameShort",
                table: "TeamNameHistory");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "TeamNameHistory");

            migrationBuilder.CreateTable(
                name: "YoutubeMatchExtracts",
                columns: table => new
                {
                    YoutubeVideoId = table.Column<string>(type: "text", nullable: false, comment: "Can begin with uppercase letters, numbers, lowercase letters, - and _ , appending single quotation to handle this."),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Team1 = table.Column<string>(type: "text", nullable: true),
                    Team2 = table.Column<string>(type: "text", nullable: true),
                    Tournament = table.Column<string>(type: "text", nullable: true),
                    GameWeekIdentifier = table.Column<string>(type: "text", nullable: true),
                    GameDayIdentifier = table.Column<string>(type: "text", nullable: true),
                    Season = table.Column<string>(type: "text", nullable: true),
                    IsSeries = table.Column<bool>(type: "boolean", nullable: false),
                    GameNumber = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YoutubeMatchExtracts", x => x.YoutubeVideoId);
                    table.ForeignKey(
                        name: "FK_YoutubeMatchExtracts_YoutubeVideoResults_YoutubeVideoId",
                        column: x => x.YoutubeVideoId,
                        principalTable: "YoutubeVideoResults",
                        principalColumn: "YoutubeVideoId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YoutubeMatchExtracts");

            migrationBuilder.AddColumn<string>(
                name: "CurrentNameShort",
                table: "TeamNameHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "TeamNameHistory",
                type: "text",
                nullable: true);
        }
    }
}
