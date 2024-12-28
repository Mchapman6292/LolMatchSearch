using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProcessed_TeamNameHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameHistory",
                table: "Processed_TeamNameHistory");

            migrationBuilder.AddColumn<DateTime>(
                name: "ChangeDate",
                table: "Processed_TeamNameHistory",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChangeDepth",
                table: "Processed_TeamNameHistory",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ChangeType",
                table: "Processed_TeamNameHistory",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChangedTo",
                table: "Processed_TeamNameHistory",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParentOrganization",
                table: "Processed_TeamNameHistory",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PreviousName",
                table: "Processed_TeamNameHistory",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangeDate",
                table: "Processed_TeamNameHistory");

            migrationBuilder.DropColumn(
                name: "ChangeDepth",
                table: "Processed_TeamNameHistory");

            migrationBuilder.DropColumn(
                name: "ChangeType",
                table: "Processed_TeamNameHistory");

            migrationBuilder.DropColumn(
                name: "ChangedTo",
                table: "Processed_TeamNameHistory");

            migrationBuilder.DropColumn(
                name: "ParentOrganization",
                table: "Processed_TeamNameHistory");

            migrationBuilder.DropColumn(
                name: "PreviousName",
                table: "Processed_TeamNameHistory");

            migrationBuilder.AddColumn<List<string>>(
                name: "NameHistory",
                table: "Processed_TeamNameHistory",
                type: "text[]",
                nullable: true);
        }
    }
}
