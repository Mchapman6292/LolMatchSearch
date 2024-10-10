using LolMatchFilterNew.Application.Configuration.StartConfiguration;
using Microsoft.Extensions.Hosting;
using LolMatchFilterNew.Domain.Apis.LeaguepediaDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.ILeaguepediaDataFetcher;
using Microsoft.Extensions.DependencyInjection;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaMatchDetailRepository;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IJsonConverters;
using Newtonsoft.Json.Linq;
using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaMatchDetailRepository;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryService;
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
                var leaguepediaDataFetcher = scope.ServiceProvider.GetRequiredService<ILeaguepediaDataFetcher>();
                var leaguepediaRepository = scope.ServiceProvider.GetRequiredService<ILeaguepediaMatchDetailRepository>();
                var jsonConverter = scope.ServiceProvider.GetRequiredService<IJsonConverter>();
                var logger = scope.ServiceProvider.GetRequiredService<IAppLogger>();
                var matchRepository = scope.ServiceProvider.GetRequiredService<ILeaguepediaMatchDetailRepository>();
                var leaguepediaQueryService = scope.ServiceProvider.GetRequiredService<ILeaguepediaQueryService>();

                string query = leaguepediaQueryService.BuildQueryStringForPlayersChampsInSeason("LEC 2023 Summer Season");

                IEnumerable<JObject> apiData = await leaguepediaDataFetcher.FetchAndExtractMatches(query);

                int addedEntries = await leaguepediaRepository.BulkAddLeaguepediaMatchDetails(apiData);

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