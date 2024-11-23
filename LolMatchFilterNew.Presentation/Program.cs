using LolMatchFilterNew.Application.Configuration.StartConfiguration;
using Microsoft.Extensions.Hosting;
using LolMatchFilterNew.Domain.Apis.LeaguepediaDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.ILeaguepediaDataFetcher;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using LolMatchFilterNew.Domain.Entities.Import_ScoreboardGamesEntities;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryServices;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaApiMappers;
using System.Reflection;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IAPIControllers;
using LolMatchFilterNew.Domain.YoutubeDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.IYoutubeDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IYoutubeVideoRepository;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaMatchDetailRepository;
using LolMatchFilterNew.Domain.Entities.Import_YoutubeDataEntities;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IYoutubeController;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.ITeamHistoryLogic;
using LolMatchFilterNew.Infrastructure.Repositories.TeamRenameRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamRenameRepositories;




// TO DO
// Get all known Team abbreviations using TeamRedirects?
// Find method responsible for appending ' to YoutubePlaylistIds.
//





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
                var teamHistoryLogic = scope.ServiceProvider.GetRequiredService<ITeamHistoryLogic>();
                var teamRenameRepository = scope.ServiceProvider.GetRequiredService<ITeamRenameRepository>();



                await APIController.ControllerAddLpediaTeamsToDatabase();












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