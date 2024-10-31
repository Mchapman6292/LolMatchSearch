using LolMatchFilterNew.Application.Configuration.StartConfiguration;
using Microsoft.Extensions.Hosting;
using LolMatchFilterNew.Domain.Apis.LeaguepediaDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.ILeaguepediaDataFetcher;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryServices;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaApiMappers;
using System.Reflection;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IAPIControllers;
using LolMatchFilterNew.Domain.YoutubeDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.IYoutubeDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IYoutubeVideoRepository;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaMatchDetailRepository;
using LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IYoutubeController;





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
                var leaguepediaApiMapper = scope.ServiceProvider.GetRequiredService<ILeaguepediaApiMapper>();
                var logger = scope.ServiceProvider.GetRequiredService<IAppLogger>();
                var matchRepository = scope.ServiceProvider.GetRequiredService<ILeaguepediaMatchDetailRepository>();
                var leaguepediaQueryService = scope.ServiceProvider.GetRequiredService<ILeaguepediaQueryService>();
                var APIController = scope.ServiceProvider.GetRequiredService<IAPIControllers>();
                var youtubeFetcher = scope.ServiceProvider.GetRequiredService<IYoutubeDataFetcher>();
                var youtubeRepository = scope.ServiceProvider.GetRequiredService<IYoutubeVideoRepository>();
                var youtubeController = scope.ServiceProvider.GetRequiredService<IYoutubeController>();

                string leagueName = "EMEA";

                var playlistIds = new List<string>
                    {
                        "PLJwuLHutaYuKP5Pmd8Ry233MM0jO4j6m1"

                    };

                await APIController.FetchAndAddTeamNamesForLeague(leagueName);

         

                










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