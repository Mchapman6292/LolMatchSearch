using Newtonsoft.Json.Linq;
using System.Web;
using LolMatchFilterNew.Domain.Interfaces.ILeaguepediaDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IActivityService;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryService;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaAPILimiter;
using Activity = System.Diagnostics.Activity;
using System.Text.Json;
using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using System;
using Npgsql.PostgresTypes;

namespace LolMatchFilterNew.Domain.Apis.LeaguepediaDataFetcher
{
    // API limits : 500 results per query
    // From help page "Please add a small delay between queries (1-2 seconds). If the server gets stressed as a result of a huge number of queries, you may be rate-limited for some time."

    public class LeaguepediaDataFetcher : ILeaguepediaDataFetcher
    {
        private readonly IAppLogger _appLogger;
        private readonly IActivityService _activityService;
        private readonly IApiHelper _apiHelper;
        private readonly ILeaguepediaQueryService _leaguepediaQueryService;
        private readonly ILeaguepediaAPILimiter _leaguepediaApiLimiter;
        private readonly IHttpClientFactory _httpClientFactory;
        const int MaxResultsPerQuery = 490;




        private static readonly HttpClient client = new HttpClient();
        private static readonly string SaveDirectory = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
    "LolMatchReports");

        public LeaguepediaDataFetcher( IAppLogger appLogger, IActivityService activityService, IApiHelper apiHelper, ILeaguepediaQueryService leaguepediaQueryService, ILeaguepediaAPILimiter leaguepediaAPILimiter, IHttpClientFactory httpClientFactory)
        {
            _appLogger = appLogger;
            _activityService = activityService;
            _apiHelper = apiHelper;
            _leaguepediaQueryService = leaguepediaQueryService;
            _leaguepediaApiLimiter = leaguepediaAPILimiter;
            _httpClientFactory = httpClientFactory;
        }

        // Fetches data from the Leaguepedia API and returns the response as a JObject.
        public async Task<JObject> FetchLeaguepediaApiResponse(string urlQuery)
        {
            await _leaguepediaApiLimiter.WaitForNextRequestAsync();
            try
            {
                
                using var client = _httpClientFactory.CreateClient();
                using var response = await client.GetAsync(urlQuery);
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                return JObject.Parse(content);
            }
            catch (HttpRequestException ex)
            {
                _appLogger.Error($"HTTP request failed for Leaguepedia API. URL: {urlQuery}", ex);
                throw;
            }
            catch (JsonException ex)
            {
                _appLogger.Error($"JSON parsing failed for Leaguepedia API. URL: {urlQuery}", ex);
                throw;
            }
        }

        // Extracts the matches from the "cargoquery" field in the JSON response and returns them as a list of JObject.
        // 
        public IEnumerable<JObject> ExtractMatchesFromLeaguepediaApiResponse(JObject jsonMatchData)
        {
            var cargoQueryData = jsonMatchData["cargoquery"] as JArray;

            if (cargoQueryData == null || !cargoQueryData.Any())
            {
                _appLogger.Info("No match data found in the API response.");
                return new List<JObject>();
            }
            var extractedMatches = cargoQueryData
                .Cast<JObject>()
                .Where(match => match != null);
                
 
            _appLogger.Info($"Extracted {extractedMatches.Count()} matches from the API response.");
            return extractedMatches;
        }


        // Fetches & extracts one page of matches. 
        public async Task<IEnumerable<JObject>> FetchPageOfMatches(string baseUrl, int offset, int limit)
        {
            string url = $"{baseUrl}&offset={offset}&limit={limit}";
            var apiResponse = await FetchLeaguepediaApiResponse(url);
            return ExtractMatchesFromLeaguepediaApiResponse(apiResponse);
        }

        // Fetches and accumulates matches from the API, handling pagination until maxResults is reached or no more data is available.
        public async Task<IEnumerable<JObject>> FetchAndExtractMatches(string baseUrl, int maxResults = MaxResultsPerQuery)
        {

            _appLogger.Info($"URL: {baseUrl}.");

            var allMatches = new List<JObject>();
            int offset = 0;

            while (allMatches.Count < maxResults)
            {
                var pageMatches = await FetchPageOfMatches(baseUrl, offset, MaxResultsPerQuery);

                if (pageMatches.Count() == 0)
                {
                    _appLogger.Info("No more matches found. Ending pagination.");
                    break;
                }

                allMatches.AddRange(pageMatches.Take(maxResults - allMatches.Count));
                _appLogger.Info($"Fetched {pageMatches.Count()} matches. Total matches so far: {allMatches.Count}");

                if (pageMatches.Count() < MaxResultsPerQuery)
                {
                    _appLogger.Info("Reached end of data. Ending pagination.");
                    break;
                }

                offset += pageMatches.Count();
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

                string url = _leaguepediaQueryService.BuildQueryStringForPlayersChampsInSeason(tournament, currentChunkSize, offset);
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
                       .Cast<JObject>()
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
            var firstResult = allMatches.FirstOrDefault();

            foreach(var results in firstResult)
            {
                _appLogger.Info($" data: {results}.");  
            }
            
            return allMatches;
        }
    }


}
