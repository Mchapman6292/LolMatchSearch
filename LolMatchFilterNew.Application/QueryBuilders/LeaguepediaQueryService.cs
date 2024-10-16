﻿using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryService;
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


        public string BuildQueryStringForPlayersChampsInSeason(string tournamentName, int limit = 490, int offset = 0)
        {
            // Ensure the tournament name is properly escaped/ url encoded.

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["action"] = "cargoquery";
            query["format"] = "json";
            query["tables"] = "ScoreboardGames=SG,ScoreboardPlayers=SP";
            query["join_on"] = "SG.GameId=SP.GameId";
            query["fields"] = "SG.GameId,SG.Gamename, SG.DateTime_UTC, SG.Tournament, SG.Team1, SG.Team2, " +
                              "SG.Team1Players, SG.Team2Players, SG.Team1Picks, SG.Team2Picks, SG.WinTeam, SG.LossTeam, SG.Team1Kills, SG.Team2Kills";
            query["where"] = $"SG.Tournament = '{tournamentName}'";
            query["group_by"] = "SG.GameId";
            query["order_by"] = "SG.DateTime_UTC ASC";
            query["limit"] = limit.ToString();
            query["offset"] = offset.ToString();
            return $"{BaseUrl}?{query}";
        }
    }
}
