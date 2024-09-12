using Newtonsoft.Json.Linq;
using System.Web;
using LolMatchFilterNew.Domain.Interfaces.ILeaguepediaApis;
using LolMatchFilterNew.Domain.Interfaces.IHttpJsonServices;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IActivityService;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using Activity = System.Diagnostics.Activity;

namespace LolMatchFilterNew.Domain.Apis.LeaguepediaApis
{
    public class LeaguepediaApi : ILeaguepediaApi
    {
        private readonly IAppLogger _appLogger;
        private readonly IHttpJsonService _httpJsonService;
        private readonly IActivityService _activityService;
        private readonly IApiHelper _apiHelper;
        private static readonly HttpClient client = new HttpClient();
        private static readonly string SaveDirectory = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
    "LolMatchReports");

        public LeaguepediaApi(IHttpJsonService httpJsonService, IAppLogger appLogger, IActivityService activityService, IApiHelper apiHelper)
        {
            _httpJsonService = httpJsonService;
            _appLogger = appLogger;
            _activityService = activityService;
            _apiHelper = apiHelper;
        }


        public async Task GetCapsAhriMatchesAsync()
        {
            Activity activity = null;
            try
            {
                activity = await _activityService.StartActivityAsync(nameof(GetCapsAhriMatchesAsync));

                var query = HttpUtility.ParseQueryString(string.Empty);
                query["action"] = "cargoquery";
                query["tables"] = "ScoreboardPlayers=SP,ScoreboardGames=SG";
                query["join_on"] = "SP.GameId=SG.GameId";
                query["fields"] = "SP.GameId,SG.DateTime_UTC,SG.Tournament,SG.Team1,SG.Team2,SP.Champion,SP.Role,SG.Team1Players,SG.Team2Players,SG.Team1Picks,SG.Team2Picks";
                query["where"] = "SP.Link='Caps' AND SP.Champion='Ahri'";
                query["order_by"] = "SG.DateTime_UTC DESC";
                query["limit"] = "5";
                query["format"] = "json";
                string baseUrl = "https://lol.fandom.com/api.php";
                string url = $"{baseUrl}?{query}";

                _appLogger.Info($"Constructed URL: {url}. TraceId: {activity.TraceId}, SpanId: {activity.SpanId}");

                Activity fetchActivity = null;
                try
                {
                    fetchActivity = await _activityService.StartChildActivityAsync(activity, "FetchJsonData");
                    JObject json = await _httpJsonService.FetchJsonDataAsync(fetchActivity, url);

                    if (json["error"] != null)
                    {
                        _appLogger.Warning($"API Error: {json["error"]["info"]}. TraceId: {activity.TraceId}, SpanId: {activity.SpanId}");
                        await _apiHelper.WriteToDocxDocumentAsync(activity, $"API Error: {json["error"]["info"]}");
                        return;
                    }

                    var results = json["cargoquery"];
                    if (results != null && results.HasValues)
                    {
                        List<string> matchDetails = new List<string>();
                        foreach (var result in results)
                        {
                            matchDetails.Add(result["title"].ToString());
                        }

                        Activity writeActivity = null;
                        try
                        {
                            writeActivity = await _activityService.StartChildActivityAsync(activity, "WriteToDocx");
                            string filePath = await _apiHelper.WriteToDocxDocumentAsync(writeActivity, "Caps Ahri Matches", matchDetails);
                            _appLogger.Info($"Match report saved to: {filePath}. TraceId: {activity.TraceId}, SpanId: {activity.SpanId}");
                            Console.WriteLine($"Match report saved to: {filePath}");
                        }
                        finally
                        {
                            if (writeActivity != null)
                                await _activityService.StopActivityAsync(writeActivity);
                        }
                    }
                    else
                    {
                        _appLogger.Warning($"No matches found for the given criteria. TraceId: {activity.TraceId}, SpanId: {activity.SpanId}");
                        await _apiHelper.WriteToDocxDocumentAsync(activity, "No matches found for the given criteria.");
                    }
                }
                finally
                {
                    if (fetchActivity != null)
                        await _activityService.StopActivityAsync(fetchActivity);
                }
            }
            catch (HttpRequestException e)
            {
                _appLogger.Error($"Error fetching data: {e.Message}. TraceId: {activity?.TraceId}, SpanId: {activity?.SpanId}", e);
                await _apiHelper.WriteToDocxDocumentAsync(activity, $"Error fetching data: {e.Message}");
            }
            catch (Newtonsoft.Json.JsonException e)
            {
                _appLogger.Error($"Error parsing JSON: {e.Message}. TraceId: {activity?.TraceId}, SpanId: {activity?.SpanId}", e);
                await _apiHelper.WriteToDocxDocumentAsync(activity, $"Error parsing JSON: {e.Message}");
            }
            catch (Exception e)
            {
                _appLogger.Error($"Unexpected error in {nameof(GetCapsAhriMatchesAsync)}: {e.Message}. TraceId: {activity?.TraceId}, SpanId: {activity?.SpanId}", e);
                await _apiHelper.WriteToDocxDocumentAsync(activity, $"Unexpected error: {e.Message}");
            }
            finally
            {
                if (activity != null)
                    await _activityService.StopActivityAsync(activity);
            }
        }


       


    }
}
