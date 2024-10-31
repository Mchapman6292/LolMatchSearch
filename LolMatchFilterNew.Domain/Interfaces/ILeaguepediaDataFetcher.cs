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

        Task<IEnumerable<JObject>> FetchPageOfResults(string urlQuery);
        IEnumerable<JObject> ExtractDataFromLeaguepediaApiResponse(JObject jsonMatchData);


        Task<IEnumerable<JObject>> FetchAndExtractMatches(string leagueName, int? numberOfPages = null, int queryLimit = 490);





    }
}
