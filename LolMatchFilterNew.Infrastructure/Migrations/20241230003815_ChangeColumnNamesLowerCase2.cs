using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnNamesLowerCase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "processed_youtubeData");

            migrationBuilder.RenameTable(
                name: "processed_ProPlayers",
                newName: "processed_proplayers");

            migrationBuilder.RenameTable(
                name: "processed_LeagueTeamEntity",
                newName: "processed_leagueteamentity");

            migrationBuilder.RenameTable(
                name: "import_YoutubeData",
                newName: "import_youtubeData");

            migrationBuilder.RenameTable(
                name: "import_TeamsTable",
                newName: "import_teamstable");

            migrationBuilder.RenameTable(
                name: "import_TeamRenames",
                newName: "import_teamrenames");

            migrationBuilder.RenameTable(
                name: "import_TeamRedirect",
                newName: "import_teamredirect");

            migrationBuilder.RenameTable(
                name: "import_Teamname",
                newName: "import_teamname");

            migrationBuilder.RenameTable(
                name: "import_ScoreboardGames",
                newName: "import_scoreboardGames");

            migrationBuilder.AddPrimaryKey(
                name: "PK_processed_youtubeData",
                table: "processed_youtubeData",
                column: "YoutubeVideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_processed_proplayers",
                table: "processed_proplayers",
                column: "LeaguepediaPlayerAllName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_processed_leagueteamentity",
                table: "processed_leagueteamentity",
                column: "TeamName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_import_youtubeData",
                table: "import_youtubeData",
                column: "YoutubeVideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_import_teamstable",
                table: "import_teamstable",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_import_teamrenames",
                table: "import_teamrenames",
                columns: new[] { "OriginalName", "NewName", "Date" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_import_teamredirect",
                table: "import_teamredirect",
                columns: new[] { "PageName", "AllName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_import_teamname",
                table: "import_teamname",
                column: "TeamnameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_import_scoreboardGames",
                table: "import_scoreboardGames",
                columns: new[] { "GameName", "GameId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_processed_youtubeData",
                table: "processed_youtubeData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_processed_proplayers",
                table: "processed_proplayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_processed_leagueteamentity",
                table: "processed_leagueteamentity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_import_youtubeData",
                table: "import_youtubeData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_import_teamstable",
                table: "import_teamstable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_import_teamrenames",
                table: "import_teamrenames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_import_teamredirect",
                table: "import_teamredirect");

            migrationBuilder.DropPrimaryKey(
                name: "PK_import_teamname",
                table: "import_teamname");

            migrationBuilder.DropPrimaryKey(
                name: "PK_import_scoreboardGames",
                table: "import_scoreboardGames");

            migrationBuilder.RenameTable(
                name: "processed_youtubeData",
                newName: "processed_YoutubeData");

            migrationBuilder.RenameTable(
                name: "processed_proplayers",
                newName: "processed_ProPlayers");

            migrationBuilder.RenameTable(
                name: "processed_leagueteamentity",
                newName: "processed_LeagueTeamEntity");

            migrationBuilder.RenameTable(
                name: "import_youtubeData",
                newName: "import_YoutubeData");

            migrationBuilder.RenameTable(
                name: "import_teamstable",
                newName: "import_TeamsTable");

            migrationBuilder.RenameTable(
                name: "import_teamrenames",
                newName: "import_TeamRenames");

            migrationBuilder.RenameTable(
                name: "import_teamredirect",
                newName: "import_TeamRedirect");

            migrationBuilder.RenameTable(
                name: "import_teamname",
                newName: "import_Teamname");

            migrationBuilder.RenameTable(
                name: "import_scoreboardGames",
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
    }
}
