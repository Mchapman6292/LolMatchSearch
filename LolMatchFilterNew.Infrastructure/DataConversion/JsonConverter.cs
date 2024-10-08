using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IJsonConverters;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LolMatchFilterNew.Infrastructure.DataConversion.JsonConverters
{
    public class JsonConverter : IJsonConverter
    {
        private readonly IAppLogger _appLogger;


        public JsonConverter(IAppLogger appLogger)
        {
            _appLogger = appLogger;
        }


        public async Task<IEnumerable<LeaguepediaMatchDetailEntity>> DeserializeLeaguepediaJsonData(IEnumerable<JObject> leaguepediaData)
        {
            if (leaguepediaData == null || !leaguepediaData.Any())
            {
                _appLogger.Error($"Input data cannot be null or empty for {nameof(DeserializeLeaguepediaJsonData)}.");
                throw new ArgumentNullException(nameof(leaguepediaData), "Input data cannot be null or empty.");
            }
            return await Task.Run(() =>
            {
                var results = new List<LeaguepediaMatchDetailEntity>();
                int processedCount = 0;
                foreach (var matchData in leaguepediaData)
                {
                    processedCount++;
                    try
                    {
                        _appLogger.Info($"Processing match data {processedCount}:");
                        _appLogger.Info($"Raw data: {matchData}");

                        var entity = new LeaguepediaMatchDetailEntity
                        {
                            LeaguepediaGameIdAndTitle = GetStringValue(matchData, "GameId"),
                            DateTimeUTC = ParseDateTime(matchData, "DateTime UTC"),
                            Tournament = GetStringValue(matchData, "Tournament"),
                            Team1 = GetStringValue(matchData, "Team1"),
                            Team2 = GetStringValue(matchData, "Team2"),
                            Team1Picks = GetStringValue(matchData, "Team1Picks").Split(',').Select(s => s.Trim()).ToList(),
                            Team2Picks = GetStringValue(matchData, "Team2Picks").Split(',').Select(s => s.Trim()).ToList()
                        };

                        _appLogger.Info($"Deserialized entity {processedCount}:");
                        _appLogger.Info($"  GameId: {entity.LeaguepediaGameIdAndTitle}");
                        _appLogger.Info($"  DateTime: {entity.DateTimeUTC}");
                        _appLogger.Info($"  Tournament: {entity.Tournament}");
                        _appLogger.Info($"  Team1: {entity.Team1}");
                        _appLogger.Info($"  Team2: {entity.Team2}");
                        _appLogger.Info($"  Team1Picks: {string.Join(", ", entity.Team1Picks)}");
                        _appLogger.Info($"  Team2Picks: {string.Join(", ", entity.Team2Picks)}");

                        results.Add(entity);
                    }
                    catch (Exception ex)
                    {
                        _appLogger.Error($"Error processing match data {processedCount}: {ex.Message}");
                        _appLogger.Error($"Raw data: {matchData}");
                    }
                }

                _appLogger.Info($"Deserialized {results.Count} entities out of {processedCount} processed.");
                return results;
            });
        }

        private string GetStringValue(JObject obj, string key)
        {
            JToken targetObj = obj;
            if (obj.ContainsKey("title") && obj["title"] is JObject titleObj)
            {
                targetObj = titleObj;
                _appLogger.Info($"Using nested 'title' object for key: {key}");
            }

            var token = targetObj[key];
            var result = token?.ToString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(result))
            {
                _appLogger.Warning($"Empty or null value for key: {key}. Token type: {token?.Type.ToString() ?? "null"}");
            }
            else
            {
                _appLogger.Info($"GetStringValue for key '{key}': '{result}'");
            }

            return result;
        }

        private DateTime ParseDateTime(JObject obj, string key)
        {
            try
            {
                JToken targetObj = obj;
                if (obj.ContainsKey("title") && obj["title"] is JObject titleObj)
                {
                    targetObj = titleObj;
                    _appLogger.Info("Found nested 'title' object, using it for parsing.");
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

                _appLogger.Info($"Attempting to parse DateTime for key '{key}' with value: '{rawValue}'.");
                if (DateTime.TryParse(rawValue, out DateTime result))
                {
                    if (result.Kind != DateTimeKind.Utc)
                    {
                        result = DateTime.SpecifyKind(result, DateTimeKind.Utc);
                    }
                    _appLogger.Info($"Successfully parsed DateTime for key '{key}': {result} (UTC)");
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


    }
}

