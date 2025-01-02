using LolMatchFilterNew.Application.Configuration.StartConfiguration;
using Microsoft.Extensions.Hosting;
using LolMatchFilterNew.Domain.Apis.LeaguepediaDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.ILeaguepediaDataFetcher;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryServices;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaApiMappers;
using System.Reflection;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IAPIControllers;
using LolMatchFilterNew.Domain.YoutubeService.YoutubeDataFetchers;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.IYoutubeDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_ScoreboardGamesRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IYoutubeController;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_TeamRenameRepositories;
using Domain.Interfaces.ApplicationInterfaces.ITeamnameDTOBuilders;
using Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers;
using Domain.DTOs.Western_MatchDTOs;
using System.Collections.Generic;
using Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers;




// TO DO
// Get all known Team abbreviations using TeamRedirects?
// Find method responsible for appending ' to YoutubePlaylistIds.
// Update Teams to use teamRedirects
// Need to ensure all Youtube & scoreboardGame mappers & repositories use updated composite key.





namespace LolMatchFilterNew.Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Executing Main Application Program.cs");
            var host = await StartConfiguration.InitializeApplicationAsync(args);
            using (var scope = host.Services.CreateScope())
            {
                var leaguepediaDataFetcher = scope.ServiceProvider.GetRequiredService<ILeaguepediaDataFetcher>();
                var leaguepediaRepository = scope.ServiceProvider.GetRequiredService<IImport_ScoreboardGamesRepository>();
                var leaguepediaApiMapper = scope.ServiceProvider.GetRequiredService<ILeaguepediaApiMapper>();
                var logger = scope.ServiceProvider.GetRequiredService<IAppLogger>();
                var scoreboardGamesRepository = scope.ServiceProvider.GetRequiredService<IImport_ScoreboardGamesRepository>();
                var leaguepediaQueryService = scope.ServiceProvider.GetRequiredService<ILeaguepediaQueryService>();
                var APIController = scope.ServiceProvider.GetRequiredService<IAPIControllers>();
                var youtubeFetcher = scope.ServiceProvider.GetRequiredService<IYoutubeDataFetcher>();
                var youtubeRepository = scope.ServiceProvider.GetRequiredService<IImport_YoutubeDataRepository>();
                var youtubeController = scope.ServiceProvider.GetRequiredService<IYoutubeController>();
                var teamRenameRepository = scope.ServiceProvider.GetRequiredService<IProcessed_TeamNameHistoryRepository>();
                var teamnameDTOBuilder = scope.ServiceProvider.GetRequiredService<ITeamnameDTOBuilder>();
                var testFunctions = scope.ServiceProvider.GetRequiredService<IStoredSqlFunctionCaller>();
                var objectLogger = scope.ServiceProvider.GetRequiredService<IObjectLogger>();   


                List<string> MainTeamsExcludingChina = new List<string> { "LoL EMEA Championship", "Europe League Championship Series", "League of Legends Championship Series", "LoL Champions Korea" };


                IEnumerable<WesternMatchDTO> matches = await testFunctions.GetWesternMatches();

                int processed = 0;

                foreach (var match in matches)
                {
                    if(processed % 50 == 0)
                    {
                        objectLogger.LogWesternMatchDTO(match);

                    }
                    processed++;
                }



                Console.ReadLine();









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