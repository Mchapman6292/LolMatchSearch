using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LolMatchFilterNew.Application.QueryBuilders.LeaguepediaQueryService
{
    public class LeaguepediaQueryService : ILeaguepediaQueryService
    {

        // From leaguedpia API docs - 500 result per query, Please add a small delay between queries (1-2 seconds).

        // Example tournament format "LEC 2023 Summer Season",
        private const string BaseUrl = "https://lol.fandom.com/api.php";
        private int MaxResultsPerQuery = 490;


        public string BuildQueryStringForPlayersChampsInSeason(string tournamentName, int queryLimit, int offset = 0)
        {
            // Ensure the tournament name is properly escaped/ url encoded.

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["action"] = "cargoquery";
            query["format"] = "json";
            query["tables"] = "ScoreboardGames=SG,ScoreboardPlayers=SP,Tournaments=T";
            query["join_on"] = "SG.GameId=SP.GameId,T.Name=SG.Tournament";
            query["fields"] = "SG.GameId,SG.Gamename, T.League, SG.DateTime_UTC, SG.Tournament, SG.Team1, SG.Team2, " +
                              "SG.Team1Players, SG.Team2Players, SG.Team1Picks, SG.Team2Picks, SG.WinTeam, SG.LossTeam, SG.Team1Kills, SG.Team2Kills";
            query["where"] = $"T.League = '{tournamentName}'";
            query["group_by"] = "SG.GameId";
            query["order_by"] = "SG.DateTime_UTC ASC";
            query["limit"] = queryLimit.ToString();
            query["offset"] = offset.ToString();
            return $"{BaseUrl}?{query}";

        }

        public string BuildQueryStringForTeamsInRegion(string region,int queryLimit, int offset = 0)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["action"] = "cargoquery";
            query["format"] = "json";
            query["tables"] = "Teams";
            query["fields"] = "Name,Short,Region,RenamedTo";
            query["where"] = $"Region = '{region}'";
            query["limit"] = queryLimit.ToString();
            query["offset"] = offset.ToString();
            return $"{BaseUrl}?{query}";
        }


    }
}
