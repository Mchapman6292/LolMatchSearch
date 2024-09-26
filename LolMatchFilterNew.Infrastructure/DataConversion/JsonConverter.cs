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


        public async Task<IAsyncEnumerable<LeaguepediaMatchDetailEntity>> ConvertJsonToLeaguepediaEntity(IEnumerable<string> jsonStrings)
        {
            return jsonStrings.ToAsyncEnumerable().Select(jsonString =>
            {
                try
                {
                    var jsonObject = JObject.Parse(jsonString);
                    return MapToEntity(jsonObject);
                }
                catch (Exception ex)
                {
                    _appLogger.Error($"Error parsing JSON: {ex.Message}");
                    return null;
                }
            }).Where(entity => entity != null);
        }
    }







}
