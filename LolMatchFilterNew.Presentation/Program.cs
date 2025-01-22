using Domain.Interfaces.ApplicationInterfaces.IMatchDTOServices.IImport_TeamNameServices;
using Domain.Interfaces.ApplicationInterfaces.ITeamNameDTOBuilders;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamNameServices;
using Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers;
using Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers;
using LolMatchFilterNew.Application.Configuration.StartConfiguration;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IAPIControllers;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchServiceControllers;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IYoutubeController;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryServices;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.IYoutubeDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.ILeaguepediaDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_ScoreboardGamesRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_TeamRenameRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaApiMappers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;









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
                var import_ScoreboardGamesRepository = scope.ServiceProvider.GetRequiredService<IImport_ScoreboardGamesRepository>();
                var leaguepediaQueryService = scope.ServiceProvider.GetRequiredService<ILeaguepediaQueryService>();
                var APIController = scope.ServiceProvider.GetRequiredService<IAPIControllers>();
                var youtubeFetcher = scope.ServiceProvider.GetRequiredService<IYoutubeDataFetcher>();
                var youtubeRepository = scope.ServiceProvider.GetRequiredService<IImport_YoutubeDataRepository>();
                var youtubeController = scope.ServiceProvider.GetRequiredService<IYoutubeController>();
                var teamRenameRepository = scope.ServiceProvider.GetRequiredService<IProcessed_TeamNameHistoryRepository>();
                var teamnameDTOBuilder = scope.ServiceProvider.GetRequiredService<ITeamNameDTOBuilder>();
                var testFunctions = scope.ServiceProvider.GetRequiredService<IStoredSqlFunctionCaller>();
                var objectLogger = scope.ServiceProvider.GetRequiredService<IObjectLogger>();
                var sqlFunctionCaller = scope.ServiceProvider.GetRequiredService<IStoredSqlFunctionCaller>();
                var matchComparisonController = scope.ServiceProvider.GetRequiredService<IMatchComparisonController>();
                var import_TeamNameService = scope.ServiceProvider.GetRequiredService<IImport_TeamNameService>();
                var youtubeTeamNameService = scope.ServiceProvider.GetRequiredService<IYoutubeTeamNameService>();




                await matchComparisonController.TESTFindTeamNameMatchesInYoutubeTitleAsync();











                Console.ReadKey();




            }
            await host.RunAsync();
        }




    }
}