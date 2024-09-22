using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LolMatchFilterNew.Application.QueryBuilders.LeaguepediaQueryService
{
    public class LeaguepediaQueryService<T> : ILeaguepediaQueryService
    {

        private const string BaseUrl = "https://lol.fandom.com/api.php";
        public string BuildLeaguepediaQuery(string tournamentName, int limit = 5)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["action"] = "cargoquery";
            query["tables"] = "ScoreboardPlayers=SP,ScoreboardGames=SG";
            query["join_on"] = "SP.LeaguepediaGameIdAndTitle=SG.LeaguepediaGameIdAndTitle";
            query["fields"] = "SP.LeaguepediaGameIdAndTitle,SG.DateTime_UTC,SG.Tournament,SG.Team1,SG.Team2,SP.Champion,SP.Role,SG.Team1Players,SG.Team2Players,SG.Team1Picks,SG.Team2Picks";
            query["where"] = $"SG.Tournament='{tournamentName}'";
            query["order_by"] = "SG.DateTime_UTC DESC";
            query["limit"] = limit.ToString();
            query["format"] = "json";

            return $"{BaseUrl}?{query}";
        }

