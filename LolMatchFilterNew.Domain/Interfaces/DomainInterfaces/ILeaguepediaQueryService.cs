﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryServices
{
    public interface ILeaguepediaQueryService
    {
        string BuildQueryStringScoreBoardGames(int queryLimit, int offset = 0);

        string BuildCompleteScoreboardGamesQuery(int queryLimit, int offset = 0);

        string BuildQueryStringTeamRedirects(int queryLimit, int offset = 0);

        string BuildQueryStringForTeamsInRegion(string region, int queryLimit, int offset = 0);

        string BuildQueryForTeamNameAndAbbreviation(string leagueName, int queryLimit, int offset = 0);

        string FormatCargoQuery(string rawQuery, int queryLimit = 490, int offset = 0);

        string BuildQueryForAllFieldsInLpediaTeams(int queryLimit, int offset = 0);

        string  BuildQueryForAllResultsInLpediaTeams(int queryLimit, int offset = 0);
    }
}
