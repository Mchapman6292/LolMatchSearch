using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryServices
{
    public interface ILeaguepediaQueryService
    {
        string FormatCargoQuery(string rawQuery, int queryLimit = 490, int offset = 0);


        string BuildQueryStringScoreBoardGames(int queryLimit, int offset = 0);


        string BuildQueryStringTeamRedirects(int queryLimit, int offset = 0);


        string BuildQueryStringTeamRenames(int queryLimit, int offset = 0);


        string BuildQueryStringTeams(int queryLimit = 0, int offset = 0);




    }
}
