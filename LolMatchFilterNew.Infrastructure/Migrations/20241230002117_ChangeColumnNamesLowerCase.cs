using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnNamesLowerCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Processed_YoutubeData",
                table: "Processed_YoutubeData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Processed_ProPlayers",
                table: "Processed_ProPlayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Processed_LeagueTeamEntity",
                table: "Processed_LeagueTeamEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Import_YoutubeData",
                table: "Import_YoutubeData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Import_TeamsTable",
                table: "Import_TeamsTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Import_TeamRenames",
                table: "Import_TeamRenames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Import_TeamRedirect",
                table: "Import_TeamRedirect");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Import_Teamname",
                table: "Import_Teamname");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Import_ScoreboardGames",
                table: "Import_ScoreboardGames");


            migrationBuilder.RenameTable(
                name: "Processed_YoutubeData",
                newName: "processed_YoutubeData");

            migrationBuilder.RenameTable(
                name: "Processed_ProPlayers",
                newName: "processed_ProPlayers");

            migrationBuilder.RenameTable(
                name: "Processed_LeagueTeamEntity",
                newName: "processed_LeagueTeamEntity");

            migrationBuilder.RenameTable(
                name: "Import_YoutubeData",
                newName: "import_YoutubeData");

            migrationBuilder.RenameTable(
                name: "Import_TeamsTable",
                newName: "import_TeamsTable");

            migrationBuilder.RenameTable(
                name: "Import_TeamRenames",
                newName: "import_TeamRenames");

            migrationBuilder.RenameTable(
                name: "Import_TeamRedirect",
                newName: "import_TeamRedirect");

            migrationBuilder.RenameTable(
                name: "Import_Teamname",
                newName: "import_Teamname");

            migrationBuilder.RenameTable(
                name: "Import_ScoreboardGames",
                newName: "import_ScoreboardGames");

  

            migrationBuilder.AddPrimaryKey(
                name: "PK_processed_YoutubeData",
                table: "processed_YoutubeData",
                column: "YoutubeVideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_processed_ProPlayers",
                table: "processed_ProPlayers",
                column: "LeaguepediaPlayerAllName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_processed_LeagueTeamEntity",
                table: "processed_LeagueTeamEntity",
                column: "TeamName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_import_YoutubeData",
                table: "import_YoutubeData",
                column: "YoutubeVideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_import_TeamsTable",
                table: "import_TeamsTable",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_import_TeamRenames",
                table: "import_TeamRenames",
                columns: new[] { "OriginalName", "NewName", "Date" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_import_TeamRedirect",
                table: "import_TeamRedirect",
                columns: new[] { "PageName", "AllName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_import_Teamname",
                table: "import_Teamname",
                column: "TeamnameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_import_ScoreboardGames",
                table: "import_ScoreboardGames",
                columns: new[] { "GameName", "GameId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_processed_YoutubeData",
                table: "processed_YoutubeData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_processed_ProPlayers",
                table: "processed_ProPlayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_processed_LeagueTeamEntity",
                table: "processed_LeagueTeamEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_import_YoutubeData",
                table: "import_YoutubeData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_import_TeamsTable",
                table: "import_TeamsTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_import_TeamRenames",
                table: "import_TeamRenames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_import_TeamRedirect",
                table: "import_TeamRedirect");

            migrationBuilder.DropPrimaryKey(
                name: "PK_import_Teamname",
                table: "import_Teamname");

            migrationBuilder.DropPrimaryKey(
                name: "PK_import_ScoreboardGames",
                table: "import_ScoreboardGames");


            migrationBuilder.RenameTable(
                name: "processed_YoutubeData",
                newName: "Processed_YoutubeData");

            migrationBuilder.RenameTable(
                name: "processed_ProPlayers",
                newName: "Processed_ProPlayers");

            migrationBuilder.RenameTable(
                name: "processed_LeagueTeamEntity",
                newName: "Processed_LeagueTeamEntity");

            migrationBuilder.RenameTable(
                name: "import_YoutubeData",
                newName: "Import_YoutubeData");

            migrationBuilder.RenameTable(
                name: "import_TeamsTable",
                newName: "Import_TeamsTable");

            migrationBuilder.RenameTable(
                name: "import_TeamRenames",
                newName: "Import_TeamRenames");

            migrationBuilder.RenameTable(
                name: "import_TeamRedirect",
                newName: "Import_TeamRedirect");

            migrationBuilder.RenameTable(
                name: "import_Teamname",
                newName: "Import_Teamname");

            migrationBuilder.RenameTable(
                name: "import_ScoreboardGames",
                newName: "Import_ScoreboardGames");


            migrationBuilder.AddPrimaryKey(
                name: "PK_Processed_YoutubeData",
                table: "Processed_YoutubeData",
                column: "YoutubeVideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Processed_ProPlayers",
                table: "Processed_ProPlayers",
                column: "LeaguepediaPlayerAllName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Processed_LeagueTeamEntity",
                table: "Processed_LeagueTeamEntity",
                column: "TeamName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Import_YoutubeData",
                table: "Import_YoutubeData",
                column: "YoutubeVideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Import_TeamsTable",
                table: "Import_TeamsTable",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Import_TeamRenames",
                table: "Import_TeamRenames",
                columns: new[] { "OriginalName", "NewName", "Date" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Import_TeamRedirect",
                table: "Import_TeamRedirect",
                columns: new[] { "PageName", "AllName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Import_Teamname",
                table: "Import_Teamname",
                column: "TeamnameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Import_ScoreboardGames",
                table: "Import_ScoreboardGames",
                columns: new[] { "GameName", "GameId" });
        }
    }
}
