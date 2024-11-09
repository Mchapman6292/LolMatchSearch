using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLpediaTeams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LOLTeams",
                columns: table => new
                {
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    OverviewPage = table.Column<string>(type: "character varying(2083)", maxLength: 2083, nullable: true),
                    Short = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Location = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TeamLocation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Region = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    OrganizationPage = table.Column<string>(type: "character varying(2083)", maxLength: 2083, nullable: true),
                    Image = table.Column<string>(type: "character varying(2083)", maxLength: 2083, nullable: true),
                    Twitter = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Youtube = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Facebook = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Instagram = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Discord = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Snapchat = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Vk = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Subreddit = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Website = table.Column<string>(type: "text", nullable: true),
                    RosterPhoto = table.Column<string>(type: "character varying(2083)", maxLength: 2083, nullable: true),
                    IsDisbanded = table.Column<bool>(type: "boolean", nullable: false),
                    RenamedTo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsLowercase = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOLTeams", x => x.Name);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LOLTeams");
        }
    }
}
