using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LolMatchFilterNew.Domain.Interfaces.ILeaguepediaDataFetcher
{
    public interface ILeaguepediaDataFetcher
    {
        const int MaxResultsPerQuery = 490;
        Task<JObject> FetchLeaguepediaApiResponse(string urlQuery);
        IEnumerable<JObject> ExtractMatchesFromLeaguepediaApiResponse(JObject jsonMatchData);
        Task<IEnumerable<JObject>> FetchPageOfMatches(string tournamentName, string urlQuery);
        Task<IEnumerable<JObject>> FetchAndExtractMatches(string tournamentName, int? numberOfPages = null, int queryLimit = 490);





    }
}
