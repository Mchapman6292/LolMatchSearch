using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xceed.Words.NET;
using LolMatchFilterNew.Domain.ILeaguepediaApis;
using LolMatchFilterNew.Domain.Interfaces.IHttpJsonServices;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using System.Diagnostics;

namespace LolMatchFilterNew.Domain.Apis
{
    public class LeaguepediaApi : ILeaguepediaApi
    {
        private readonly IAppLogger _appLogger;
        private readonly IHttpJsonService _httpJsonService;
        private static readonly HttpClient client = new HttpClient();
        private static readonly string SaveDirectory = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
    "LolMatchReports");

        public LeaguepediaApi(IHttpJsonService httpJsonService, IAppLogger appLogger)
        {
            _httpJsonService = httpJsonService;
            _appLogger = appLogger;
        }


        public async Task GetCapsAhriMatchesAsync(Activity activity)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["action"] = "cargoquery";
            query["tables"] = "ScoreboardPlayers=SP,ScoreboardGames=SG";
            query["join_on"] = "SP.GameId=SG.GameId";
            query["fields"] = "SP.GameId,SG.DateTime_UTC,SG.Tournament,SG.Team1,SG.Team2,SP.Champion,SP.Role, SG.Team1Players, SG.Team2Players, SG.Team1Picks, SG.Team2Picks";
            query["where"] = "SP.Link='Caps' AND SP.Champion='Ahri'";
            query["order_by"] = "SG.DateTime_UTC DESC";
            query["limit"] = "5";
            query["format"] = "json";

            string baseUrl = "https://lol.fandom.com/api.php";
            string url = $"{baseUrl}?{query}";

            client.DefaultRequestHeaders.UserAgent.ParseAdd("CSharpLeaguepediaBot/1.0");

            try
            {
                JObject json = await _httpJsonService.FetchJsonDataAsync(activity, url);

                if (json["error"] != null)
                {
                    await WriteToDocxDocumentAsync($"API Error: {json["error"]["info"]}");
                }
                else
                {
                    var results = json["cargoquery"];
                    if (results != null && results.HasValues)
                    {
                        List<string> matchDetails = new List<string>();
                        foreach (var result in results)
                        {
                            matchDetails.Add(result["title"].ToString());
                        }
                        string filePath = await WriteToDocxDocumentAsync("Caps Ahri Matches", matchDetails);
                        Console.WriteLine($"Match report saved to: {filePath}");
                    }
                    else
                    {
                        await WriteToDocxDocumentAsync("No matches found for the given criteria.");
                    }
                }
            }
            catch (HttpRequestException e)
            {
                await WriteToDocxDocumentAsync($"Error fetching data: {e.Message}");
            }
            catch (Newtonsoft.Json.JsonException e)
            {
                await WriteToDocxDocumentAsync($"Error parsing JSON: {e.Message}");
            }
        }


        private async Task<string> WriteToDocxDocumentAsync(string title, List<string> content = null)
        {
            _appLogger.Info($"Starting WriteToDocxDocumentAsync with title: {title}");
            _appLogger.Info($"Content items: {content?.Count ?? 0}");

            try
            {
                Directory.CreateDirectory(SaveDirectory);
                _appLogger.Info($"Save directory created/verified: {SaveDirectory}");

                string fileName = $"CapsAhriMatches_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.docx";
                string fullPath = Path.Combine(SaveDirectory, fileName);
                _appLogger.Info($"Full file path: {fullPath}");

                using (var document = DocX.Create(fullPath))
                {
                    _appLogger.Info("DocX document created");

                    document.InsertParagraph(title).Bold();
                    _appLogger.Info("Title inserted and bolded");

                    if (content != null)
                    {
                        _appLogger.Info("Adding content to document");
                        foreach (var item in content)
                        {
                            document.InsertParagraph(item);
                            _appLogger.Info($"Inserted paragraph: {item.Substring(0, Math.Min(50, item.Length))}...");
                        }
                    }
                    else
                    {
                        _appLogger.Info("No content to add to document");
                    }

                    document.Save();
                    _appLogger.Info("Document saved");
                }

                await Task.Delay(100);
                _appLogger.Info("Async delay completed");

                _appLogger.Info($"File saved to: {fullPath}");
                _appLogger.Info($"Directory: {Path.GetDirectoryName(fullPath)}");
                _appLogger.Info($"Filename: {Path.GetFileName(fullPath)}");

                return fullPath;
            }
            catch (Exception ex)
            {
                _appLogger.Info($"Error in WriteToDocxDocumentAsync: {ex.Message}");
                _appLogger.Info($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }


    }
}
