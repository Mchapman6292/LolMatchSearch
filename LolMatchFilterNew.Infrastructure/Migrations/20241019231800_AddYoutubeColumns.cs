using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddYoutubeColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_YoutubeVideoResults",
                table: "YoutubeVideoResults");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "YoutubeVideoResults");

            migrationBuilder.AlterColumn<string>(
                name: "YoutubeResultHyperlink",
                table: "YoutubeVideoResults",
                type: "character varying(2083)",
                maxLength: 2083,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "YoutubeVideoResults",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishedAt",
                table: "YoutubeVideoResults",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "YoutubeVideoResults",
                type: "character varying(2083)",
                maxLength: 2083,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "League",
                table: "LeaguepediaMatchDetails",
                type: "text",
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_YoutubeVideoResults",
                table: "YoutubeVideoResults",
                column: "YoutubeVideoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_YoutubeVideoResults",
                table: "YoutubeVideoResults");

            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "YoutubeVideoResults");

            migrationBuilder.DropColumn(
                name: "League",
                table: "LeaguepediaMatchDetails");

            migrationBuilder.AlterColumn<string>(
                name: "YoutubeResultHyperlink",
                table: "YoutubeVideoResults",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(2083)",
                oldMaxLength: 2083);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "YoutubeVideoResults",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishedAt",
                table: "YoutubeVideoResults",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "YoutubeVideoResults",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_YoutubeVideoResults",
                table: "YoutubeVideoResults",
                column: "Id");
        }
    }
}
