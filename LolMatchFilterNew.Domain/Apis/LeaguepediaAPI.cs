using Newtonsoft.Json.Linq;
using System.Web;
using LolMatchFilterNew.Domain.Interfaces.ILeaguepediaApis;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IActivityService;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryService;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaAPILimiter;
using Activity = System.Diagnostics.Activity;
using System.Text.Json;

namespace LolMatchFilterNew.Domain.Apis.LeaguepediaApis
{
    // API limits : 500 results per query
    // From help page "Please add a small delay between queries (1-2 seconds). If the server gets stressed as a result of a huge number of queries, you may be rate-limited for some time."

    public class LeaguepediaApi : ILeaguepediaApi
    {
        private readonly IAppLogger _appLogger;
        private readonly IActivityService _activityService;
        private readonly IApiHelper _apiHelper;
        private readonly ILeaguepediaQueryService _leaguepediaQueryService;
        private readonly ILeaguepediaAPILimiter _leaguepediaApiLimiter;
        private readonly IHttpClientFactory _httpClientFactory;


        private static readonly HttpClient client = new HttpClient();
        private static readonly string SaveDirectory = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
    "LolMatchReports");

        public LeaguepediaApi( IAppLogger appLogger, IActivityService activityService, IApiHelper apiHelper, ILeaguepediaQueryService leaguepediaQueryService, ILeaguepediaAPILimiter leaguepediaAPILimiter, IHttpClientFactory httpClientFactory)
        {
            _appLogger = appLogger;
            _activityService = activityService;
            _apiHelper = apiHelper;
            _leaguepediaQueryService = leaguepediaQueryService;
            _leaguepediaApiLimiter = leaguepediaAPILimiter;
            _httpClientFactory = httpClientFactory;
        }


        public async Task GetMatchesForSplit(string tournament, string split, int year)
        {
            _appLogger.Info($"STarting {nameof(GetMatchesForSplit)}.");

            string query = _leaguepediaQueryService.BuildLeaguepediaQuery(tournament, split, year);








        }



        public async Task<IEnumerable<JObject>> FetchLeaguepediaMatchesAsync(string url)
        {
            _appLogger.Info($"Fetching Leaguepedia matches from URL: {url}");
            try
            {
                using var client = _httpClientFactory.CreateClient();
                using var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                var result = JObject.Parse(content);

                var matchesData = result["cargoquery"] as JArray;
                if (matchesData == null || !matchesData.Any())
                {
                    _appLogger.Warning($"No match data found in the Leaguepedia API response. URL: {url}");
                    return Enumerable.Empty<JObject>();
                }

                var extractedMatches = matchesData
                    .Select(match => match["title"] as JObject)
                    .Where(match => match != null)
                    .ToList();

                _appLogger.Info($"Successfully fetched {extractedMatches.Count} matches from Leaguepedia API");
                return extractedMatches;
            }
            catch (HttpRequestException ex)
            {
                _appLogger.Error($"HTTP request failed for Leaguepedia API: {url}", ex);
                throw;
            }
            catch (JsonException ex)
            {
                _appLogger.Error($"JSON parsing failed for Leaguepedia API response: {url}", ex);
                throw;
            }
        }





    }
}
