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
                foreach (var matchData in leaguepediaData)
                {
                    try
                    {
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
                        results.Add(entity);
                    }
                    catch (Exception ex)
                    {
                        _appLogger.Error($"Error processing match data: {ex.Message}.");
                    }
                }
                return results;
            });
        }

        private string GetStringValue(JObject obj, string key)
        {
            return obj[key]?.ToString() ?? string.Empty;
        }

        private DateTime ParseDateTime(JObject obj, string key)
        {
            if (DateTime.TryParse(obj[key]?.ToString(), out DateTime result))
            {
                return result;
            }
            _appLogger.Warning($"Failed to parse DateTime for key '{key}'. Using default value.");
            return DateTime.MinValue;
        }
    }







}


