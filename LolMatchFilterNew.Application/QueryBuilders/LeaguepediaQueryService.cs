using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryService;
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

        // Example tournament format "LEC 2023 Summer Season",
        private const string BaseUrl = "https://lol.fandom.com/api.php";

     
        public string BuildLeaguepediaQuery(string tournamentName, int limit = 480, int offset = 0)
        {
            // Ensure the tournament name is properly escaped/ url encoded.

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["action"] = "cargoquery";
            query["format"] = "json";
            query["tables"] = "ScoreboardGames=SG,ScoreboardPlayers=SP";
            query["join_on"] = "SG.GameId=SP.GameId";
            query["fields"] = "SG.GameId, SG.DateTime_UTC, SG.Tournament, SG.Team1, SG.Team2, " +
                              "SG.Winner, SG.Team1Picks, SG.Team2Picks";
            query["where"] = $"SG.Tournament = '{tournamentName}'";
            query["group_by"] = "SG.GameId";
            query["order_by"] = "SG.DateTime_UTC ASC";
            query["limit"] = limit.ToString();
            query["offset"] = offset.ToString();
            return $"{BaseUrl}?{query}";
        }
    }
}
