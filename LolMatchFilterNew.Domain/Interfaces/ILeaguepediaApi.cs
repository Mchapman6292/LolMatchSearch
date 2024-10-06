using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LolMatchFilterNew.Domain.Interfaces.ILeaguepediaApis
{
    public interface ILeaguepediaApi
    {
        Task<IEnumerable<JObject>> FetchLeaguepediaMatchesAsync(string tournament);

        Task<IEnumerable<JObject>> FetchLeaguepediaMatchesForTestingAsync(string tournament, int maxResults);

    }
}
