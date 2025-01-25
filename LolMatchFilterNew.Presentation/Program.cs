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

/* Current Problems
 
 * Move factory/builder classes with only one method to the service class?

 * YoutubeTitleTeamOccurenceDTO NEEDS FIX/MOVED
    - Change to DTO and service class instead of current YoutubeTitleTeamOccurenceDTO doing everything. 


 * TEAMNAME VS LONGNAME MISMATCH
  - Team abbreviation in database but not in Youtube video -   Solved, match was occuring but other false positives had more hits

  - IsExactWordOccurrence only returns the first occurrence of a team input. 
    Can return an incorrect team name if matched too soon.
    


 * e.g Vitality vs Splyce Highlights | EU LCS Week 9 Day 2 Spring 2016 S6 | VIT vs SPY, Database name = Team Vitality
  - Solutions
     Exclude date format to reduce 
    Define Tournaments/Teams to narrow list of potential matches?
    Get a Teams all known opponents from SG and use that as the search parameters?
    Define a new table in database, slowly add videos for each playlist, using smaller search window?


* DTO Classes/Teams Data structure. 
* Linking Matching Teams in YoutubeTitleTeamOccurenceDTO to Teams or TeamName?
* Having different types of dtos used in Application for teams will cause confusion later.
* Decide what data should be held within One/Two team classes/DTOs.
 */





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