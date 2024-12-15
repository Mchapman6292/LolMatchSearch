using Newtonsoft.Json.Linq;
using System.Web;
using LolMatchFilterNew.Domain.Interfaces.ILeaguepediaDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IActivityService;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryServices;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaAPILimiter;
using Activity = System.Diagnostics.Activity;
using Newtonsoft.Json; 
using System.Text.Json;
using System;
using Npgsql.PostgresTypes;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using LolMatchFilterNew.Domain.Helpers.ApiHelper;
using static System.Net.WebRequestMethods;

namespace LolMatchFilterNew.Domain.Apis.LeaguepediaDataFetcher
{
    // API limits : 500 results per urlQuery
    // From help page "Please add a small delay between queries (1-2 seconds). If the server gets stressed as a result of a huge number of queries, you may be rate-limited for some time."

    public class LeaguepediaDataFetcher : ILeaguepediaDataFetcher
    {
        private readonly IAppLogger _appLogger;
        private readonly IActivityService _activityService;
        private readonly IApiHelper _apiHelper;
        private readonly ILeaguepediaQueryService _leaguepediaQueryService;
        private readonly ILeaguepediaAPILimiter _leaguepediaApiLimiter;
        private readonly IHttpClientFactory _httpClientFactory;
        const int QueryLimit = 490;
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
            catch (System.Text.Json.JsonException ex)
            {
                _appLogger.Error($"JSON parsing failed for Leaguepedia API. URL: {urlQuery}", ex);
                throw;
            }
        }

        // Extracts the matches from the "cargoquery" field in the JSON response and returns them as a list of JObject
        public IEnumerable<JObject> ExtractDataFromLeaguepediaApiResponse(JObject jsonMatchData)
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

        public async Task<IEnumerable<JObject>> FetchPageOfResults(string urlQuery)
        {
            var apiResponse = await FetchLeaguepediaApiResponse(urlQuery);
            return ExtractDataFromLeaguepediaApiResponse(apiResponse);

        }






        // Fetches and accumulates matches from the API, handling pagination until QueryLimit is reached or no more data is available.
        public async Task<IEnumerable<JObject>> FetchAndExtractMatches(int? numberOfPages = null, int queryLimit = 490)
        {
            
            var allMatches = new List<JObject>();
            try
            {
                int offset = 0;
                bool hasMoreData = true;
                int? totalLimit = numberOfPages.HasValue ? numberOfPages.Value * queryLimit : null;
                int totalMatchesCount = 0;

                _appLogger.Info($"Starting match fetch for {nameof(FetchAndExtractMatches)} Pages: {numberOfPages}, Limit: {queryLimit}");

                while (hasMoreData && (!totalLimit.HasValue || allMatches.Count < totalLimit.Value))
                {
                    try
                    {
                        int currentQueryLimit = totalLimit.HasValue
                            ? Math.Min(queryLimit, totalLimit.Value - allMatches.Count)
                        : queryLimit;

                        string rawQuery = _leaguepediaQueryService.BuildQueryStringTeams(queryLimit, offset);


                        string urlQuery = _leaguepediaQueryService.FormatCargoQuery(rawQuery, currentQueryLimit, offset);

                        _appLogger.Info($"Fetching page with offset {offset}, limit {currentQueryLimit}");
                        _appLogger.Debug($"Generated URL: {urlQuery}");

                        var pageOfMatches = await FetchPageOfResults(urlQuery);
                        totalMatchesCount += pageOfMatches.Count();

                        if (!pageOfMatches.Any())
                        {
                            _appLogger.Info($"No more matches found at offset {offset}. Total matches fetched: {totalMatchesCount}");

                            if (allMatches.Any())
                            {
                                var first = allMatches.First();
                                var last = allMatches.Last();
                                _appLogger.Debug($"First match: {first.ToString(Formatting.Indented)}");
                                _appLogger.Debug($"Last match: {last.ToString(Formatting.Indented)}");
                            }
                            break;
                        }

                        allMatches.AddRange(pageOfMatches);
                        hasMoreData = pageOfMatches.Count() > 0;
                        offset += pageOfMatches.Count();

                        _appLogger.Info($"Successfully fetched {pageOfMatches.Count()} matches. Total so far: {allMatches.Count}");
                    }
                    catch (HttpRequestException ex)
                    {
                        _appLogger.Error($"HTTP request failed at offset {offset}: {ex.Message}");
                        _appLogger.Error($"Stack trace: {ex.StackTrace}");
                        throw;
                    }
                    catch (JsonReaderException ex)
                    {
                        _appLogger.Error($"JSON parsing error at offset {offset}: {ex.Message}");
                        _appLogger.Error($"Path: {ex.Path}, LineNumber: {ex.LineNumber}, LinePosition: {ex.LinePosition}");
                        throw;
                    }
                }
            }
            catch (PostgresException pgEx)
            {
                _appLogger.Error($"Database error occurred: {pgEx.GetDetailedErrorMessage()}");
                throw;
            }
            catch (DbUpdateException dbEx)
            {
                if (dbEx.InnerException is PostgresException pgEx)
                {
                    _appLogger.Error($"Database update failed: {pgEx.GetDetailedErrorMessage()}");
                }
                else
                {
                    _appLogger.Error($"Database update failed: {dbEx.Message}");
                    _appLogger.Error($"Inner exception: {dbEx.InnerException?.Message}");
                }
                throw;
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Unexpected error in FetchAndExtractMatches: {ex.Message}");
                _appLogger.Error($"Stack trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    _appLogger.Error($"Inner exception: {ex.InnerException.Message}");
                    _appLogger.Error($"Inner stack trace: {ex.InnerException.StackTrace}");
                }
                throw;
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

                string url = _leaguepediaQueryService.BuildQueryStringScoreBoardGames(chunkSize, offset);
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
                catch (System.Text.Json.JsonException ex)
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
