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

            return await Task.Run(() => leaguepediaData.Select(matchData =>
            {
                var title = matchData["title"] as JObject;
                return new LeaguepediaMatchDetailEntity
                {
                    LeaguepediaGameIdAndTitle = title["GameId"].ToString(),
                    DateTimeUTC = DateTime.Parse(title["DateTime UTC"].ToString()),
                    Tournament = title["Tournament"].ToString(),
                    Team1 = title["Team1"].ToString(),
                    Team2 = title["Team2"].ToString(),
                    Team1Picks = title["Team1Picks"].ToString().Split(',').ToList(),
                    Team2Picks = title["Team2Picks"].ToString().Split(',').ToList()
                };
            }).ToList());
        }


    
    
    
    
    
    }
}

