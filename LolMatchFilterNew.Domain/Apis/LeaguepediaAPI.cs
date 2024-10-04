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




        public async Task<IEnumerable<JObject>> FetchLeaguepediaMatchesAsync(string tournament)
        {
            const int chunkSize = 500; // API limit
            var allMatches = new List<JObject>();
            int offset = 0;

            while (true)
            {
                await _leaguepediaApiLimiter.WaitForNextRequestAsync();

                string url = _leaguepediaQueryService.BuildLeaguepediaQuery(tournament, offset);
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
                        _appLogger.Info($"No more match data found. Total matches fetched: {allMatches.Count}");
                        break;
                    }

                    var extractedMatches = matchesData
                        .Select(match => match["title"] as JObject)
                        .Where(match => match != null)
                        .ToList();

                    allMatches.AddRange(extractedMatches);
                    _appLogger.Info($"Fetched {extractedMatches.Count} matches. Total matches so far: {allMatches.Count}");

                    if (extractedMatches.Count < chunkSize)
                    {
                        _appLogger.Info($"Reached end of data. Total matches fetched: {allMatches.Count}");
                        break;
                    }

                    offset += chunkSize;
                }
                catch (HttpRequestException ex)
                {
                    _appLogger.Error($"HTTP request failed for Leaguepedia API. URL: {url}");
                    throw;
                }
                catch (JsonException ex)
                {
                    _appLogger.Error($"JSON parsing failed for Leaguepedia API. URL: {url}");
                    throw;
                }
            }

            return allMatches;
        }


        public async Task<IEnumerable<JObject>> FetchLeaguepediaMatchesForTestingAsync(string tournament, int maxResults)
        {
            const int chunkSize = 100; 
            var allMatches = new List<JObject>();
            int offset = 0;

            while (allMatches.Count < maxResults)
            {
                await _leaguepediaApiLimiter.WaitForNextRequestAsync();

                int remainingResults = maxResults - allMatches.Count;
                int currentChunkSize = Math.Min(chunkSize, remainingResults);

                string url = _leaguepediaQueryService.BuildLeaguepediaQuery(tournament, currentChunkSize, offset);
                _appLogger.Info($"[TEST] Fetching Leaguepedia matches from URL: {url}");

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
                        _appLogger.Info($"[TEST] No more match data found. Total matches fetched: {allMatches.Count}");
                        break;
                    }

                    var extractedMatches = matchesData
                        .Select(match => match["title"] as JObject)
                        .Where(match => match != null)
                        .Take(remainingResults)
                        .ToList();

                    allMatches.AddRange(extractedMatches);
                    _appLogger.Info($"[TEST] Fetched {extractedMatches.Count} matches. Total matches so far: {allMatches.Count}");

                    if (extractedMatches.Count < currentChunkSize || allMatches.Count >= maxResults)
                    {
                        _appLogger.Info($"[TEST] Reached end of data or max results. Total matches fetched: {allMatches.Count}");
                        break;
                    }

                    offset += extractedMatches.Count;
                }
                catch (HttpRequestException ex)
                {
                    _appLogger.Error($"[TEST] HTTP request failed for Leaguepedia API. URL: {url}");
                    throw;
                }
                catch (JsonException ex)
                {
                    _appLogger.Error($"[TEST] JSON parsing failed for Leaguepedia API. URL: {url}");
                    throw;
                }
            }

            return allMatches;
        }
    }


}
