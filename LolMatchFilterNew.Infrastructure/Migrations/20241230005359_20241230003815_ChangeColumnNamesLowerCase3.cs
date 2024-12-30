using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LolMatchFilterNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _20241230003815_ChangeColumnNamesLowerCase3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_processed_youtubeData",
                table: "processed_youtubeData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_import_youtubeData",
                table: "import_youtubeData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_import_scoreboardGames",
                table: "import_scoreboardGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_processed_leagueteamentity",
                table: "processed_leagueteamentity");

            migrationBuilder.RenameTable(
                name: "processed_youtubeData",
                newName: "processed_youtubedata");

            migrationBuilder.RenameTable(
                name: "import_youtubeData",
                newName: "import_youtubedata");

            migrationBuilder.RenameTable(
                name: "import_scoreboardGames",
                newName: "import_scoreboardgames");

            migrationBuilder.RenameTable(
                name: "processed_leagueteamentity",
                newName: "processed_leagueteam");

            migrationBuilder.RenameColumn(
                name: "Tournament",
                table: "processed_youtubedata",
                newName: "tournament");

            migrationBuilder.RenameColumn(
                name: "Season",
                table: "processed_youtubedata",
                newName: "season");

            migrationBuilder.RenameColumn(
                name: "VideoTitle",
                table: "processed_youtubedata",
                newName: "video_title");

            migrationBuilder.RenameColumn(
                name: "Team2Short",
                table: "processed_youtubedata",
                newName: "team2_short");

            migrationBuilder.RenameColumn(
                name: "Team2Long",
                table: "processed_youtubedata",
                newName: "team2_long");

            migrationBuilder.RenameColumn(
                name: "Team1Short",
                table: "processed_youtubedata",
                newName: "team1_short");

            migrationBuilder.RenameColumn(
                name: "Team1Long",
                table: "processed_youtubedata",
                newName: "team1_long");

            migrationBuilder.RenameColumn(
                name: "PublishedAt_utc",
                table: "processed_youtubedata",
                newName: "published_at_utc");

            migrationBuilder.RenameColumn(
                name: "PlayListTitle",
                table: "processed_youtubedata",
                newName: "playlist_title");

            migrationBuilder.RenameColumn(
                name: "PlayListId",
                table: "processed_youtubedata",
                newName: "playlist_id");

            migrationBuilder.RenameColumn(
                name: "IsSeries",
                table: "processed_youtubedata",
                newName: "is_series");

            migrationBuilder.RenameColumn(
                name: "GameWeekIdentifier",
                table: "processed_youtubedata",
                newName: "game_week_identifier");

            migrationBuilder.RenameColumn(
                name: "GameNumber",
                table: "processed_youtubedata",
                newName: "game_number");

            migrationBuilder.RenameColumn(
                name: "GameDayIdentifier",
                table: "processed_youtubedata",
                newName: "game_day_identifier");

            migrationBuilder.RenameColumn(
                name: "YoutubeVideoId",
                table: "processed_youtubedata",
                newName: "youtube_video_id");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "processed_proplayers",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "RealName",
                table: "processed_proplayers",
                newName: "real_name");

            migrationBuilder.RenameColumn(
                name: "PreviousInGameNames",
                table: "processed_proplayers",
                newName: "previous_in_game_names");

            migrationBuilder.RenameColumn(
                name: "LeaguepediaPlayerId",
                table: "processed_proplayers",
                newName: "leaguepedia_player_id");

            migrationBuilder.RenameColumn(
                name: "InGameName",
                table: "processed_proplayers",
                newName: "in_game_name");

            migrationBuilder.RenameColumn(
                name: "CurrentTeam",
                table: "processed_proplayers",
                newName: "current_team");

            migrationBuilder.RenameColumn(
                name: "LeaguepediaPlayerAllName",
                table: "processed_proplayers",
                newName: "leaguepedia_player_all_name");

            migrationBuilder.RenameColumn(
                name: "YoutubeResultHyperlink",
                table: "import_youtubedata",
                newName: "youtube_result_hyperlink");

            migrationBuilder.RenameColumn(
                name: "VideoTitle",
                table: "import_youtubedata",
                newName: "video_title");

            migrationBuilder.RenameColumn(
                name: "ThumbnailUrl",
                table: "import_youtubedata",
                newName: "thumbnail_url");

            migrationBuilder.RenameColumn(
                name: "PublishedAt_utc",
                table: "import_youtubedata",
                newName: "published_at_utc");

            migrationBuilder.RenameColumn(
                name: "PlaylistTitle",
                table: "import_youtubedata",
                newName: "playlist_title");

            migrationBuilder.RenameColumn(
                name: "PlaylistId",
                table: "import_youtubedata",
                newName: "playlist_id");

            migrationBuilder.RenameColumn(
                name: "YoutubeVideoId",
                table: "import_youtubedata",
                newName: "youtube_video_id");

            migrationBuilder.RenameColumn(
                name: "Youtube",
                table: "import_teamstable",
                newName: "youtube");

            migrationBuilder.RenameColumn(
                name: "Website",
                table: "import_teamstable",
                newName: "website");

            migrationBuilder.RenameColumn(
                name: "Vk",
                table: "import_teamstable",
                newName: "vk");

            migrationBuilder.RenameColumn(
                name: "Twitter",
                table: "import_teamstable",
                newName: "twitter");

            migrationBuilder.RenameColumn(
                name: "Subreddit",
                table: "import_teamstable",
                newName: "subreddit");

            migrationBuilder.RenameColumn(
                name: "Snapchat",
                table: "import_teamstable",
                newName: "snapchat");

            migrationBuilder.RenameColumn(
                name: "Short",
                table: "import_teamstable",
                newName: "short");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "import_teamstable",
                newName: "region");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "import_teamstable",
                newName: "location");

            migrationBuilder.RenameColumn(
                name: "Instagram",
                table: "import_teamstable",
                newName: "instagram");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "import_teamstable",
                newName: "image");

            migrationBuilder.RenameColumn(
                name: "Facebook",
                table: "import_teamstable",
                newName: "facebook");

            migrationBuilder.RenameColumn(
                name: "Discord",
                table: "import_teamstable",
                newName: "discord");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "import_teamstable",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "TeamLocation",
                table: "import_teamstable",
                newName: "team_location");

            migrationBuilder.RenameColumn(
                name: "RosterPhoto",
                table: "import_teamstable",
                newName: "roster_photo");

            migrationBuilder.RenameColumn(
                name: "RenamedTo",
                table: "import_teamstable",
                newName: "renamed_to");

            migrationBuilder.RenameColumn(
                name: "OverviewPage",
                table: "import_teamstable",
                newName: "overview_page");

            migrationBuilder.RenameColumn(
                name: "OrganizationPage",
                table: "import_teamstable",
                newName: "organization_page");

            migrationBuilder.RenameColumn(
                name: "IsLowercase",
                table: "import_teamstable",
                newName: "is_lowercase");

            migrationBuilder.RenameColumn(
                name: "IsDisbanded",
                table: "import_teamstable",
                newName: "is_disbanded");

            migrationBuilder.RenameColumn(
                name: "Verb",
                table: "import_teamrenames",
                newName: "verb");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "import_teamrenames",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "NewsId",
                table: "import_teamrenames",
                newName: "news_id");

            migrationBuilder.RenameColumn(
                name: "IsSamePage",
                table: "import_teamrenames",
                newName: "is_same_page");

            migrationBuilder.RenameColumn(
                name: "NewName",
                table: "import_teamrenames",
                newName: "new_name");

            migrationBuilder.RenameColumn(
                name: "OriginalName",
                table: "import_teamrenames",
                newName: "original_name");

            migrationBuilder.RenameColumn(
                name: "UniqueLine",
                table: "import_teamredirect",
                newName: "unique_line");

            migrationBuilder.RenameColumn(
                name: "OtherName",
                table: "import_teamredirect",
                newName: "other_name");

            migrationBuilder.RenameColumn(
                name: "AllName",
                table: "import_teamredirect",
                newName: "all_name");

            migrationBuilder.RenameColumn(
                name: "PageName",
                table: "import_teamredirect",
                newName: "page_name");

            migrationBuilder.RenameColumn(
                name: "Short",
                table: "import_teamname",
                newName: "short");

            migrationBuilder.RenameColumn(
                name: "Medium",
                table: "import_teamname",
                newName: "medium");

            migrationBuilder.RenameColumn(
                name: "Longname",
                table: "import_teamname",
                newName: "longname");

            migrationBuilder.RenameColumn(
                name: "Inputs",
                table: "import_teamname",
                newName: "inputs");

            migrationBuilder.RenameColumn(
                name: "TeamnameId",
                table: "import_teamname",
                newName: "teamname_id");

            migrationBuilder.RenameColumn(
                name: "Tournament",
                table: "import_scoreboardgames",
                newName: "tournament");

            migrationBuilder.RenameColumn(
                name: "Team2",
                table: "import_scoreboardgames",
                newName: "team2");

            migrationBuilder.RenameColumn(
                name: "Team1",
                table: "import_scoreboardgames",
                newName: "team1");

            migrationBuilder.RenameColumn(
                name: "DateTime_utc",
                table: "import_scoreboardgames",
                newName: "datetime_utc");

            migrationBuilder.RenameColumn(
                name: "WinTeam",
                table: "import_scoreboardgames",
                newName: "win_team");

            migrationBuilder.RenameColumn(
                name: "Team2Players",
                table: "import_scoreboardgames",
                newName: "team2_players");

            migrationBuilder.RenameColumn(
                name: "Team2Picks",
                table: "import_scoreboardgames",
                newName: "team2_picks");

            migrationBuilder.RenameColumn(
                name: "Team1Players",
                table: "import_scoreboardgames",
                newName: "team1_players");

            migrationBuilder.RenameColumn(
                name: "Team1Picks",
                table: "import_scoreboardgames",
                newName: "team1_picks");

            migrationBuilder.RenameColumn(
                name: "MatchId",
                table: "import_scoreboardgames",
                newName: "match_id");

            migrationBuilder.RenameColumn(
                name: "LossTeam",
                table: "import_scoreboardgames",
                newName: "loss_team");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "import_scoreboardgames",
                newName: "game_id");

            migrationBuilder.RenameColumn(
                name: "GameName",
                table: "import_scoreboardgames",
                newName: "game_name");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "processed_leagueteam",
                newName: "region");

            migrationBuilder.RenameColumn(
                name: "NameShort",
                table: "processed_leagueteam",
                newName: "name_short");

            migrationBuilder.RenameColumn(
                name: "TeamName",
                table: "processed_leagueteam",
                newName: "team_name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_processed_youtubedata",
                table: "processed_youtubedata",
                column: "youtube_video_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_import_youtubedata",
                table: "import_youtubedata",
                column: "youtube_video_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_import_scoreboardgames",
                table: "import_scoreboardgames",
                columns: new[] { "game_name", "game_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_processed_leagueteam",
                table: "processed_leagueteam",
                column: "team_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_processed_youtubedata",
                table: "processed_youtubedata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_import_youtubedata",
                table: "import_youtubedata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_import_scoreboardgames",
                table: "import_scoreboardgames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_processed_leagueteam",
                table: "processed_leagueteam");

            migrationBuilder.RenameTable(
                name: "processed_youtubedata",
                newName: "processed_youtubeData");

            migrationBuilder.RenameTable(
                name: "import_youtubedata",
                newName: "import_youtubeData");

            migrationBuilder.RenameTable(
                name: "import_scoreboardgames",
                newName: "import_scoreboardGames");

            migrationBuilder.RenameTable(
                name: "processed_leagueteam",
                newName: "processed_leagueteamentity");

            migrationBuilder.RenameColumn(
                name: "tournament",
                table: "processed_youtubeData",
                newName: "Tournament");

            migrationBuilder.RenameColumn(
                name: "season",
                table: "processed_youtubeData",
                newName: "Season");

            migrationBuilder.RenameColumn(
                name: "video_title",
                table: "processed_youtubeData",
                newName: "VideoTitle");

            migrationBuilder.RenameColumn(
                name: "team2_short",
                table: "processed_youtubeData",
                newName: "Team2Short");

            migrationBuilder.RenameColumn(
                name: "team2_long",
                table: "processed_youtubeData",
                newName: "Team2Long");

            migrationBuilder.RenameColumn(
                name: "team1_short",
                table: "processed_youtubeData",
                newName: "Team1Short");

            migrationBuilder.RenameColumn(
                name: "team1_long",
                table: "processed_youtubeData",
                newName: "Team1Long");

            migrationBuilder.RenameColumn(
                name: "published_at_utc",
                table: "processed_youtubeData",
                newName: "PublishedAt_utc");

            migrationBuilder.RenameColumn(
                name: "playlist_title",
                table: "processed_youtubeData",
                newName: "PlayListTitle");

            migrationBuilder.RenameColumn(
                name: "playlist_id",
                table: "processed_youtubeData",
                newName: "PlayListId");

            migrationBuilder.RenameColumn(
                name: "is_series",
                table: "processed_youtubeData",
                newName: "IsSeries");

            migrationBuilder.RenameColumn(
                name: "game_week_identifier",
                table: "processed_youtubeData",
                newName: "GameWeekIdentifier");

            migrationBuilder.RenameColumn(
                name: "game_number",
                table: "processed_youtubeData",
                newName: "GameNumber");

            migrationBuilder.RenameColumn(
                name: "game_day_identifier",
                table: "processed_youtubeData",
                newName: "GameDayIdentifier");

            migrationBuilder.RenameColumn(
                name: "youtube_video_id",
                table: "processed_youtubeData",
                newName: "YoutubeVideoId");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "processed_proplayers",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "real_name",
                table: "processed_proplayers",
                newName: "RealName");

            migrationBuilder.RenameColumn(
                name: "previous_in_game_names",
                table: "processed_proplayers",
                newName: "PreviousInGameNames");

            migrationBuilder.RenameColumn(
                name: "leaguepedia_player_id",
                table: "processed_proplayers",
                newName: "LeaguepediaPlayerId");

            migrationBuilder.RenameColumn(
                name: "in_game_name",
                table: "processed_proplayers",
                newName: "InGameName");

            migrationBuilder.RenameColumn(
                name: "current_team",
                table: "processed_proplayers",
                newName: "CurrentTeam");

            migrationBuilder.RenameColumn(
                name: "leaguepedia_player_all_name",
                table: "processed_proplayers",
                newName: "LeaguepediaPlayerAllName");

            migrationBuilder.RenameColumn(
                name: "youtube_result_hyperlink",
                table: "import_youtubeData",
                newName: "YoutubeResultHyperlink");

            migrationBuilder.RenameColumn(
                name: "video_title",
                table: "import_youtubeData",
                newName: "VideoTitle");

            migrationBuilder.RenameColumn(
                name: "thumbnail_url",
                table: "import_youtubeData",
                newName: "ThumbnailUrl");

            migrationBuilder.RenameColumn(
                name: "published_at_utc",
                table: "import_youtubeData",
                newName: "PublishedAt_utc");

            migrationBuilder.RenameColumn(
                name: "playlist_title",
                table: "import_youtubeData",
                newName: "PlaylistTitle");

            migrationBuilder.RenameColumn(
                name: "playlist_id",
                table: "import_youtubeData",
                newName: "PlaylistId");

            migrationBuilder.RenameColumn(
                name: "youtube_video_id",
                table: "import_youtubeData",
                newName: "YoutubeVideoId");

            migrationBuilder.RenameColumn(
                name: "youtube",
                table: "import_teamstable",
                newName: "Youtube");

            migrationBuilder.RenameColumn(
                name: "website",
                table: "import_teamstable",
                newName: "Website");

            migrationBuilder.RenameColumn(
                name: "vk",
                table: "import_teamstable",
                newName: "Vk");

            migrationBuilder.RenameColumn(
                name: "twitter",
                table: "import_teamstable",
                newName: "Twitter");

            migrationBuilder.RenameColumn(
                name: "subreddit",
                table: "import_teamstable",
                newName: "Subreddit");

            migrationBuilder.RenameColumn(
                name: "snapchat",
                table: "import_teamstable",
                newName: "Snapchat");

            migrationBuilder.RenameColumn(
                name: "short",
                table: "import_teamstable",
                newName: "Short");

            migrationBuilder.RenameColumn(
                name: "region",
                table: "import_teamstable",
                newName: "Region");

            migrationBuilder.RenameColumn(
                name: "location",
                table: "import_teamstable",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "instagram",
                table: "import_teamstable",
                newName: "Instagram");

            migrationBuilder.RenameColumn(
                name: "image",
                table: "import_teamstable",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "facebook",
                table: "import_teamstable",
                newName: "Facebook");

            migrationBuilder.RenameColumn(
                name: "discord",
                table: "import_teamstable",
                newName: "Discord");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "import_teamstable",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "team_location",
                table: "import_teamstable",
                newName: "TeamLocation");

            migrationBuilder.RenameColumn(
                name: "roster_photo",
                table: "import_teamstable",
                newName: "RosterPhoto");

            migrationBuilder.RenameColumn(
                name: "renamed_to",
                table: "import_teamstable",
                newName: "RenamedTo");

            migrationBuilder.RenameColumn(
                name: "overview_page",
                table: "import_teamstable",
                newName: "OverviewPage");

            migrationBuilder.RenameColumn(
                name: "organization_page",
                table: "import_teamstable",
                newName: "OrganizationPage");

            migrationBuilder.RenameColumn(
                name: "is_lowercase",
                table: "import_teamstable",
                newName: "IsLowercase");

            migrationBuilder.RenameColumn(
                name: "is_disbanded",
                table: "import_teamstable",
                newName: "IsDisbanded");

            migrationBuilder.RenameColumn(
                name: "verb",
                table: "import_teamrenames",
                newName: "Verb");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "import_teamrenames",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "news_id",
                table: "import_teamrenames",
                newName: "NewsId");

            migrationBuilder.RenameColumn(
                name: "is_same_page",
                table: "import_teamrenames",
                newName: "IsSamePage");

            migrationBuilder.RenameColumn(
                name: "new_name",
                table: "import_teamrenames",
                newName: "NewName");

            migrationBuilder.RenameColumn(
                name: "original_name",
                table: "import_teamrenames",
                newName: "OriginalName");

            migrationBuilder.RenameColumn(
                name: "unique_line",
                table: "import_teamredirect",
                newName: "UniqueLine");

            migrationBuilder.RenameColumn(
                name: "other_name",
                table: "import_teamredirect",
                newName: "OtherName");

            migrationBuilder.RenameColumn(
                name: "all_name",
                table: "import_teamredirect",
                newName: "AllName");

            migrationBuilder.RenameColumn(
                name: "page_name",
                table: "import_teamredirect",
                newName: "PageName");

            migrationBuilder.RenameColumn(
                name: "short",
                table: "import_teamname",
                newName: "Short");

            migrationBuilder.RenameColumn(
                name: "medium",
                table: "import_teamname",
                newName: "Medium");

            migrationBuilder.RenameColumn(
                name: "longname",
                table: "import_teamname",
                newName: "Longname");

            migrationBuilder.RenameColumn(
                name: "inputs",
                table: "import_teamname",
                newName: "Inputs");

            migrationBuilder.RenameColumn(
                name: "teamname_id",
                table: "import_teamname",
                newName: "TeamnameId");

            migrationBuilder.RenameColumn(
                name: "tournament",
                table: "import_scoreboardGames",
                newName: "Tournament");

            migrationBuilder.RenameColumn(
                name: "team2",
                table: "import_scoreboardGames",
                newName: "Team2");

            migrationBuilder.RenameColumn(
                name: "team1",
                table: "import_scoreboardGames",
                newName: "Team1");

            migrationBuilder.RenameColumn(
                name: "datetime_utc",
                table: "import_scoreboardGames",
                newName: "DateTime_utc");

            migrationBuilder.RenameColumn(
                name: "win_team",
                table: "import_scoreboardGames",
                newName: "WinTeam");

            migrationBuilder.RenameColumn(
                name: "team2_players",
                table: "import_scoreboardGames",
                newName: "Team2Players");

            migrationBuilder.RenameColumn(
                name: "team2_picks",
                table: "import_scoreboardGames",
                newName: "Team2Picks");

            migrationBuilder.RenameColumn(
                name: "team1_players",
                table: "import_scoreboardGames",
                newName: "Team1Players");

            migrationBuilder.RenameColumn(
                name: "team1_picks",
                table: "import_scoreboardGames",
                newName: "Team1Picks");

            migrationBuilder.RenameColumn(
                name: "match_id",
                table: "import_scoreboardGames",
                newName: "MatchId");

            migrationBuilder.RenameColumn(
                name: "loss_team",
                table: "import_scoreboardGames",
                newName: "LossTeam");

            migrationBuilder.RenameColumn(
                name: "game_id",
                table: "import_scoreboardGames",
                newName: "GameId");

            migrationBuilder.RenameColumn(
                name: "game_name",
                table: "import_scoreboardGames",
                newName: "GameName");

            migrationBuilder.RenameColumn(
                name: "region",
                table: "processed_leagueteamentity",
                newName: "Region");

            migrationBuilder.RenameColumn(
                name: "name_short",
                table: "processed_leagueteamentity",
                newName: "NameShort");

            migrationBuilder.RenameColumn(
                name: "team_name",
                table: "processed_leagueteamentity",
                newName: "TeamName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_processed_youtubeData",
                table: "processed_youtubeData",
                column: "YoutubeVideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_import_youtubeData",
                table: "import_youtubeData",
                column: "YoutubeVideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_import_scoreboardGames",
                table: "import_scoreboardGames",
                columns: new[] { "GameName", "GameId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_processed_leagueteamentity",
                table: "processed_leagueteamentity",
                column: "TeamName");
        }
    }
}
