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

        // Example tournamentName formats "LEC 2024 Season Finals", "LEC 2024 Spring",
        private const string BaseUrl = "https://lol.fandom.com/api.php";

        // leagueAbbreviation: Replaces tournamentName and should be the short form of the league (e.g., "LEC").
        public string BuildLeaguepediaQuery(string leagueAbbreviation, string split, int year, int limit = 480)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["action"] = "cargoquery";
            query["tables"] = "ScoreboardGames=SG,ScoreboardPlayers=SP";
            query["join_on"] = "SG.GameId=SP.GameId";
            query["fields"] = "SG.GameId, SG.DateTime_UTC, SG.Team1, SG.Team2, " +
                              "SP.Team, SP.Player, SP.Champion, SP.Role, " +
                              "SG.Winner, SG.Gamelength_Number, SG.OverviewPage";
            query["where"] = $"SG.Tournament LIKE '{leagueAbbreviation}/{year} Season/{split}%'";
            query["group_by"] = "SG.GameId, SG.DateTime_UTC, SG.Team1, SG.Team2, SP.Team, SP.Player, SP.Champion, SP.Role, SG.Winner, SG.Gamelength_Number, SG.OverviewPage";
            query["order_by"] = "SG.DateTime_UTC ASC";
            query["limit"] = limit.ToString();
            query["format"] = "json";

            return $"{BaseUrl}?{query}";
        }
    }
}
