using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryServices;
using System.Web;

namespace LolMatchFilterNew.Application.QueryBuilders.LeaguepediaQueryService
{
    public class LeaguepediaQueryService : ILeaguepediaQueryService
    {

        // From leaguedpia API docs - 500 result per query, Please add a small delay between queries (1-2 seconds).

        // Example tournament format "LEC 2023 Summer Season",
        private const string BaseUrl = "https://lol.fandom.com/api.php";
        private int MaxResultsPerQuery = 490;



        // LeagueName Metadata : https://lol.fandom.com/wiki/Metadata:Leagues

        // Database currently using main leagues only - LoL EMEA Championship(Current LEC),
        //                                            - Europe League Championship Series(Old LEC 2013-2018)
        //                                            - League of Legends Championship Series(LCS America, old)
        //                                            - LoL Champions Korea(LCK),
        //                                            






        public string FormatCargoQuery(string rawQuery, int queryLimit = 490, int offset = 0)
        {
            var uri = new Uri(rawQuery);
            var query = HttpUtility.ParseQueryString(uri.Query);

            if (query["action"] != "cargoquery" || query["format"] != "json")
            {
                throw new ArgumentException("Invalid cargo query format");
            }

            query["limit"] = queryLimit.ToString();
            query["offset"] = offset.ToString();

            return $"{BaseUrl}?{query}";
        }









        public string BuildQueryStringScoreBoardGames(int queryLimit, int offset = 0)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["action"] = "cargoquery";
            query["format"] = "json";
            query["tables"] = "ScoreboardGames=SG,ScoreboardPlayers=SP,Tournaments=T,Players=P,PlayerRedirects=PR,TeamRedirects=TR";
            query["join_on"] = "SG.GameId=SP.GameId,T.Name=SG.Tournament,SP.Link=PR.OverviewPage,PR.TargetPage=P.OverviewPage,SG.Team1=TR.AllName OR SG.Team2=TR.AllName";
            query["fields"] = "SG.GameId AS GameId,SG.Gamename AS GameName,T.League,SG.DateTime_UTC,SG.Tournament,SG.Team1,SG.Team2," +
                             "SG.Team1Players,SG.Team2Players,SG.Team1Picks,SG.Team2Picks,SG.WinTeam,SG.LossTeam,SG.Team1Kills,SG.Team2Kills";
            query["group_by"] = "SG.GameId";
            query["order_by"] = "SG.DateTime_UTC ASC";
            query["limit"] = queryLimit.ToString();
            query["offset"] = offset.ToString();
            return $"{BaseUrl}?{query}";
        }



        public string NewQuery(int queryLimit, int  queryOffset = 0)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["action"] = "cargoquery";
            query["format"] = "json";
            query["tables"] = "ScoreboardGames=SG";
            query["fields"] = "SG.Gamename,SG.GameId,SG.League,SG.DateTime_UTC,SG.Tournament,SG.Team1,SG.Team2," +
                 "SG.Team1Players,SG.Team2Players,SG.Team1Picks,SG.Team2Picks,SG.WinTeam,SG.LossTeam," +
                 "SG.Team1Kills,SG.Team2Kills";
            query["limit"] = queryLimit.ToString();
            query["offset"] = queryOffset.ToString();
            return $"{BaseUrl}?{query}";
        }
    



        // Fetches all values from ScoreboardGames without any joins to Teamredirects etc, 

        //
        public string BuildQueryStringAllScoreboardGames(int queryLimit, int offset = 0)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);

            query["action"] = "cargoquery";
            query["format"] = "json";
            query["tables"] = "ScoreboardGames=SG";

            query["fields"] = "SG.OverviewPage,SG.Tournament,SG.Team1,SG.Team2,SG.WinTeam,SG.LossTeam," +
                             "SG.DateTime_UTC,SG.DST,SG.Team1Score,SG.Team2Score,SG.Winner,SG.Gamelength," +
                             "SG.Gamelength_Number,SG.Team1Bans__full,SG.Team2Bans__full,SG.Team1Picks__full," +
                             "SG.Team2Picks__full,SG.Team1Players__full,SG.Team2Players__full,SG.Team1Dragons," +
                             "SG.Team2Dragons,SG.Team1Barons,SG.Team2Barons,SG.Team1Towers,SG.Team2Towers," +
                             "SG.Team1Gold,SG.Team2Gold,SG.Team1Kills,SG.Team2Kills,SG.Team1RiftHeralds," +
                             "SG.Team2RiftHeralds,SG.Team1VoidGrubs,SG.Team2VoidGrubs,SG.Team1Inhibitors," +
                             "SG.Team2Inhibitors,SG.Patch,SG.PatchSort,SG.MatchHistory,SG.VOD,SG.N_Page," +
                             "SG.N_MatchInTab,SG.N_MatchInPage,SG.N_GameInMatch,SG.Gamename,SG.UniqueLine," +
                             "SG.GameId,SG.MatchId,SG.RiotPlatformGameId,SG.RiotPlatformId,SG.RiotGameId," +
                             "SG.RiotHash,SG.RiotVersion";

            query["limit"] = queryLimit.ToString();
            query["offset"] = offset.ToString();

            return $"{BaseUrl}?{query}";
        }

        // Used to get all values from TeamRedirects table, need to alias _pageName due to _ not being valid in cargoquery. 
        public string BuildQueryStringTeamRedirects(int queryLimit, int offset = 0)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["action"] = "cargoquery";
            query["format"] = "json";
            query["tables"] = "TeamRedirects=MSG";
            query["fields"] = "MSG._pageName=PageName,AllName,OtherName,UniqueLine";
            query["limit"] = queryLimit.ToString();
            query["offset"] = offset.ToString();
            return $"{BaseUrl}?{query}";
        }


        // Used to add all values to Impoort_TeamRenames 12/12/2024
        public string BuildQueryStringTeamRenames(int queryLimit, int offset = 0)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["action"] = "cargoquery";
            query["format"] = "json";
            query["tables"] = "TeamRenames=TR";
            query["fields"] = "TR._pageName=PageName,TR.Date,TR.OriginalName,TR.NewName,TR.Verb,TR.Slot,TR.IsSamePage,TR.NewsId";
            query["limit"] = queryLimit.ToString();
            query["offset"] = offset.ToString();
            return $"{BaseUrl}?{query}";
        }







        public string BuildQueryStringTeams(int queryLimit = 0, int offset = 0)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["action"] = "cargoquery";
            query["format"] = "json";
            query["tables"] = "Teams=T";
            query["fields"] = "T._pageName=PageName,T.Name,T.OverviewPage,T.Short,T.Location,T.TeamLocation,T.Region,T.OrganizationPage,T.Image," +
                               "T.Twitter,T.Youtube,T.Facebook,T.Instagram,T.Bluesky,T.Discord,T.Snapchat,T.Vk,T.Subreddit,T.Website,T.RosterPhoto," +
                               "T.IsDisbanded,T.RenamedTo,T.IsLowercase";
            query["limit"] = queryLimit.ToString();
            query["offset"] = offset.ToString();
            return $"{BaseUrl}?{query}";
        }




        public string BuildQueryStringTeamsWithRedirects(int queryLimit, int offset = 0)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["action"] = "cargoquery";
            query["format"] = "json";
            query["tables"] = "Teams,TeamRedirects";// Need to join to redirects to ensure names/duplicates tracked properly
            query["join_on"] = "Teams.Name=TeamRedirects.AllName"; 
            query["fields"] = @"Teams.Name,Teams.OverviewPage,Teams.Short,Teams.Location,
                       Teams.TeamLocation,Teams.Region,Teams.OrganizationPage,
                       Teams.Image,Teams.Twitter,Teams.Youtube,Teams.Facebook,
                       Teams.Instagram,Teams.Discord,Teams.Snapchat,Teams.Vk,
                       Teams.Subreddit,Teams.Website,Teams.RosterPhoto,
                       Teams.IsDisbanded,Teams.RenamedTo,Teams.IsLowercase,
                       TeamRedirects.OtherName,TeamRedirects.UniqueLine";
            query["group_by"] = "Teams.Name"; // Group to prevent duplicate team entries
            query["limit"] = queryLimit.ToString();
            query["offset"] = offset.ToString();

            return $"{BaseUrl}?{query}";
        }











        //public string BuildQueryForTeamNameAndAbbreviation(string leagueName, int queryLimit, int offset = 0)
        //{
        //    var query = HttpUtility.ParseQueryString(string.Empty);
        //    query["action"] = "cargoquery";
        //    query["format"] = "json";
        //    query["tables"] = "Teams=Teams,TournamentResults=TR,Tournaments=T";
        //    query["join_on"] = "Teams.Name=TR.Team,TR.Event=T.Name";
        //    query["fields"] = "Teams.Name,Teams.Short,Teams.Region";
        //    query["where"] = $"T.League='{leagueName}'";
        //    query["group_by"] = "Teams.Name,Teams.Short,Teams.Region";
        //    query["limit"] = queryLimit.ToString();
        //    query["offset"] = offset.ToString();
        //    return $"{BaseUrl}?{query}";
        //}



    }
}
