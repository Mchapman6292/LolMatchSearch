using LolMatchFilterNew.Application.Configuration.StartConfiguration;
using Microsoft.Extensions.Hosting;
using LolMatchFilterNew.Domain.Apis.LeaguepediaApis;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.ILeaguepediaApis;
using Microsoft.Extensions.DependencyInjection;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaMatchDetailRepository;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IJsonConverters;
using Newtonsoft.Json.Linq;
using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using System.Reflection;

namespace LolMatchFilterNew.Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = await StartConfiguration.InitializeApplicationAsync(args);
            using (var scope = host.Services.CreateScope())
            {
                var leaguepediaApi = scope.ServiceProvider.GetRequiredService<ILeaguepediaApi>();
                var leaguepediaRepository = scope.ServiceProvider.GetRequiredService<ILeaguepediaMatchDetailRepository>();
                var jsonConverter = scope.ServiceProvider.GetRequiredService<IJsonConverter>();
                var logger = scope.ServiceProvider.GetRequiredService<IAppLogger>();

                IEnumerable<JObject> results = await leaguepediaApi.FetchLeaguepediaMatchesForTestingAsync("LEC 2023 Summer Season", 300);

                if (results == null || !results.Any())
                {
                    logger.Warning("No results fetched from LeaguepediaApi.");
                    return;
                }

                var (totalObjects, nullObjects, nullProperties) = CountObjectsAndNullProperties(results);
                logger.Info($"Fetched {totalObjects} matches. Null objects: {nullObjects}, Null properties: {nullProperties}");

                IEnumerable<LeaguepediaMatchDetailEntity> resultsEnumerable = await jsonConverter.DeserializeLeaguepediaJsonData(results);
                int addedEntries = await leaguepediaRepository.BulkAddLeaguepediaMatchDetails(resultsEnumerable);

                logger.Info($"Added {addedEntries} entries to the database.");
            }
            await host.RunAsync();
        }

        private static (int TotalObjects, int NullObjects, int NullProperties) CountObjectsAndNullProperties(IEnumerable<JObject> enumerable)
        {
            int totalObjects = 0;
            int nullObjects = 0;
            int nullProperties = 0;

            foreach (var item in enumerable)
            {
                totalObjects++;
                if (item == null)
                {
                    nullObjects++;
                    continue;
                }

                foreach (var property in item.Properties())
                {
                    if (property.Value.Type == JTokenType.Null)
                    {
                        nullProperties++;
                    }
                }
            }

            return (TotalObjects: totalObjects, NullObjects: nullObjects, NullProperties: nullProperties);
        }
    }
}