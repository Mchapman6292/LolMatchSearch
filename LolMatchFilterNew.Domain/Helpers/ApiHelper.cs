using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using Xceed.Words.NET;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using Activity = System.Diagnostics.Activity;
using Microsoft.Extensions.Configuration;
using Google.Apis.YouTube.v3.Data;
using Newtonsoft.Json.Linq;

namespace LolMatchFilterNew.Domain.Helpers.ApiHelper
{
    public class ApiHelper : IApiHelper
    {
        private readonly string _saveDirectory;
        private static int _fileCounter = 0;
        private readonly IAppLogger _appLogger;
        private static readonly string SaveDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LolMatchReports");




        public ApiHelper(IAppLogger appLogger, IConfiguration configuration)
        {
            _appLogger = appLogger;
            _saveDirectory = configuration["SaveDirectory"]
                ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LolMatchReports");
        }

        public async Task<string> WriteToDocxDocumentAsync(Activity activity, string title, List<string> content = null)
        {
            _appLogger.Info($"Starting {nameof(WriteToDocxDocumentAsync)},  Content items: {content?.Count ?? 0}, TraceId: {activity.TraceId}.");

            try
            {
                Directory.CreateDirectory(SaveDirectory);

                string fileName;
                string fullPath;

                do
                {
                    fileName = $"CapsAhriMatches_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}_{Interlocked.Increment(ref _fileCounter)}.docx";
                    fullPath = Path.Combine(_saveDirectory, fileName);
                } while (File.Exists(fullPath));

                _appLogger.Info($"Full file path for {nameof(WriteToDocxDocumentAsync)}: {fullPath}, ParentId: {activity.ParentId}, TraceId: {activity.TraceId}.");

                using (var document = DocX.Create(fullPath))
                {
                    document.InsertParagraph(title).Bold();

                    if (content != null)
                    {
                        foreach (var item in content)
                        {
                            document.InsertParagraph(item);
                        }
                    }
                    else
                    {
                        _appLogger.Info($"No content to add to document ParentId: {activity.ParentId}, TraceId: {activity.TraceId}.");
                    }
                    document.Save();
                }
                await Task.Delay(100);
                _appLogger.Info($"File saved to: {fullPath}, Directory: {Path.GetDirectoryName(fullPath)}, Filename: {Path.GetFileName(fullPath)}, ParentId:{activity.ParentId}, TraceId: {activity.TraceId}.");

                return fullPath;
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error in WriteToDocxDocumentAsync: {ex.Message}");
                _appLogger.Error($"Stack Trace: {ex.StackTrace}");
                throw;

            }
        }


        public async Task<string> WritePlaylistsToDocxDocumentAsync(IList<string> playlists)
        {
            _appLogger.Info($"Starting {nameof(WritePlaylistsToDocxDocumentAsync)}, Playlists count: {playlists?.Count ?? 0}.");
            try
            {
                Directory.CreateDirectory(_saveDirectory);
                string fileName = "LolMatchFilterPlaylistvideos.docx";
                string fullPath = Path.Combine(_saveDirectory, fileName);

                _appLogger.Info($"Full file path for {nameof(WritePlaylistsToDocxDocumentAsync)}: {fullPath}.");

                using (var document = DocX.Create(fullPath))
                {
                    document.InsertParagraph("LoL Match Filter Playlist Videos").Bold().FontSize(16);

                    if (playlists != null && playlists.Any())
                    {
                        foreach (var playlist in playlists)
                        {
                            document.InsertParagraph(playlist);
                        }
                    }
                    else
                    {
                        document.InsertParagraph("No playlists found.");
                    }

                    document.Save();
                }

                await Task.Delay(100);
                _appLogger.Info($"File saved to: {fullPath}, Directory: {Path.GetDirectoryName(fullPath)}, Filename: {Path.GetFileName(fullPath)}.");
                return fullPath;
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error in WritePlaylistsToDocxDocumentAsync: {ex.Message}");
                _appLogger.Error($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }


        public int GetInt32Value(JObject obj, string key)
        {
            try
            {
                JToken targetObj = obj;
                if (obj.ContainsKey("title") && obj["title"] is JObject titleObj)
                {
                    targetObj = titleObj;
                }

                var token = targetObj[key];
                if (token == null)
                {
                    _appLogger.Warning($"Key '{key}' does not exist in the JSON object.");
                    throw new ArgumentNullException(key, $"The value for key '{key}' is null.");
                }

                if (token.Type != JTokenType.String && token.Type != JTokenType.Integer)
                {
                    _appLogger.Warning($"Unexpected token type for key '{key}'. Type: {token.Type}");
                    throw new ArgumentException($"The value for key '{key}' is not a string or integer.");
                }

                string value = token.ToString();
                if (int.TryParse(value, out int result))
                {
                    return result;
                }
                else
                {
                    _appLogger.Error($"Failed to parse int value for key '{key}': '{value}'");
                    throw new FormatException($"The value for key '{key}' ('{value}') is not a valid integer.");
                }
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error in {nameof(GetInt32Value)} for key '{key}': {ex.Message}");
                throw;
            }
        }

        public List<string> GetValuesAsList(JObject obj, string key)
        {
            try
            {
                JToken targetObj = obj;
                if (obj.ContainsKey("title") && obj["title"] is JObject titleObj)
                {
                    targetObj = titleObj;
                }

                var token = targetObj[key];
                if (token == null)
                {
                    _appLogger.Warning($"Null value encountered for key: {key}. Returning empty list.");
                    return new List<string>();
                }
                if (token.Type == JTokenType.Array)
                {
                    return token.ToObject<List<string>>() ?? new List<string>();
                }
                if (token.Type == JTokenType.String)
                {
                    var value = token.ToString();
                    return string.IsNullOrWhiteSpace(value)
                        ? new List<string>()
                        : value.Split(',').Select(s => s.Trim()).ToList();
                }
                _appLogger.Warning($"Unexpected token type for key: {key}. Type: {token.Type}. Returning empty list.");
                return new List<string>();
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error getting list values for key: {key}. Error: {ex.Message}");
                return new List<string>();
            }
        }




        public string GetStringValue(JObject obj, string key)
        {
            JToken targetObj = obj;
            if (obj.ContainsKey("title") && obj["title"] is JObject titleObj)
            {
                targetObj = titleObj;
            }

            var token = targetObj[key];
            var result = token?.ToString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(result))
            {
                _appLogger.Warning($"Empty or null value for key: {key}. Token type: {token?.Type.ToString() ?? "null"}");
            }
            return result;
        }

        public DateTime GetDateTimeFromJObject(JObject obj, string key)
        {
            try
            {
                JToken targetObj = obj;
                if (obj.ContainsKey("title") && obj["title"] is JObject titleObj)
                {
                    targetObj = titleObj;
                }

                var token = targetObj[key];
                if (token == null)
                {
                    _appLogger.Warning($"Key '{key}' does not exist in the JSON object.");
                    return DateTime.MinValue.ToUniversalTime();
                }

                var rawValue = token.ToString();
                if (string.IsNullOrEmpty(rawValue))
                {
                    _appLogger.Warning($"Value for key '{key}' is null or empty.");
                    return DateTime.MinValue.ToUniversalTime();
                }
                if (DateTime.TryParse(rawValue, out DateTime result))
                {
                    if (result.Kind != DateTimeKind.Utc)
                    {
                        result = DateTime.SpecifyKind(result, DateTimeKind.Utc);
                    }
                    return result;
                }
                else
                {
                    _appLogger.Warning($"Failed to parse DateTime for key '{key}' with value: '{rawValue}'. Using default value (UTC).");
                    return DateTime.MinValue.ToUniversalTime();
                }
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Unexpected error while parsing DateTime for key '{key}': {ex.Message}");
                return DateTime.MinValue.ToUniversalTime();
            }
        }


        public DateTime ConvertDateTimeOffSetToUTC(object inputDateTime)
        {
            if (inputDateTime is DateTime dateTime)
            {
                return dateTime.Kind == DateTimeKind.Utc
                    ? dateTime
                    : dateTime.ToUniversalTime();
            }
            else if (inputDateTime is DateTimeOffset dateTimeOffset)
            {
                return dateTimeOffset.UtcDateTime;
            }

            throw new ArgumentException("Unsupported datetime format");
        }


    }
}

