using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IYoutubeApi;
using LolMatchFilterNew.Domain.Interfaces.ILeaguepediaDataFetcher;
using LolMatchFilterNew.Infrastructure.Logging.AppLoggers;
using Domain.Interfaces.InfrastructureInterfaces.IImport_TeamRenameRepositories;
using LolMatchFilterNew.Domain.Apis.LeaguepediaDataFetcher;
using LolMatchFilterNew.Domain.Helpers.ApiHelper;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.UnUsedYoutubeClass.YoutubeTitleMatcher;
using LolMatchFilterNew.Domain.Interfaces.IYoutubeTitleMatcher;
using LolMatchFilterNew.Infrastructure.Logging.ActivityService;
using LolMatchFilterNew.Domain.Interfaces.IActivityService;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_ScoreboardGamesRepositories;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryServices;
using LolMatchFilterNew.Application.QueryBuilders.LeaguepediaQueryService;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.ApiLimiters.LeaguepediaAPILimiter;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaAPILimiter;
using LolMatchFilterNew.Infrastructure.DataConversion.LeaguepediaApiMappers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaApiMappers;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IAPIControllers;
using LolMatchFilterNew.Infrastructure.DataConversion.YoutubeMappers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IYoutubeMapper;
using LolMatchFilterNew.Domain.YoutubeService.YoutubeDataFetchers;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.IYoutubeDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;
using LolMatchFilterNew.Application.Controllers.YoutubeControllers;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IYoutubeController;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ITeamNameHistoryFormatters;
using LolMatchFilterNew.Domain.Formatters.TeamNameHistoryFormatters;

using Infrastructure.Repositories.ImportRepositories.Import_YoutubeDataRepositories;
using Infrastructure.Repositories.ImportRepositories.Import_ScoreboardGamesRepositories;
using Infrastructure.Repositories.ImportRepositories.Import_TeamRedirectRepositories;
using Infrastructure.Repositories.ImportRepositories.Import_TeamRenameRepositories;
using Infrastructure.Repositories.ImportRepositories.Import_TeamnameRepositories;
using Domain.Interfaces.InfrastructureInterfaces.IImport_TeamnameRepositories;

using Application.MatchPairingService.ScoreboardGameService.TeamnameDTOBuilders;
using Domain.Interfaces.ApplicationInterfaces.ITeamNameDTOBuilders;
using Infrastructure.Logging.ObjectLoggers;
using Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers;
using Application.MatchPairingService.YoutubeDataService.TeamNameValidators;
using Domain.Interfaces.ApplicationInterfaces.ITeamNameValidators;
using Application.MatchPairingService.YoutubeDataService.YoutubeTeamExtractors;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamExtractors;



using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Domain.YoutubeService;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces;
using LolMatchFilterNew.Application.Controllers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_TeamRenameRepositories;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchServiceControllers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_TeamRedirectRepositories;
using Infrastructure.Repositories.ImportRepositories;
using Infrastructure.Repositories.ProcessedRepositories;
using Application.MatchPairingService.MatchComparisonResultService.MatchComparisonResultBuilders;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchComparisonResultBuilders;

using Infrastructure.SQLFunctions.StoredSqlFunctionCallers;
using Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers;
using Application.MatchPairingService.MatchComparisonResultService.MatchComparisonControllers;
using Application.MatchPairingService.YoutubeDataService.Processed_YoutubeDataDTOBuilder;
using Domain.Interfaces.ApplicationInterfaces.IProcessed_YoutubeDataDTOBuilders;
using Application.MatchPairingService.ScoreboardGameService.MatchDTOServices.TeamNameServices;
using Domain.Interfaces.ApplicationInterfaces.IMatchDTOServices.IScoreboardGamesTeamNameServices;
using Application.MatchPairingService.ScoreboardGameService.MatchDTOServices.TeamNameServices.ScoreboardGamesTeamNameServices;



namespace LolMatchFilterNew.Application.Configuration.StartConfiguration
{
    public class StartConfiguration 
    {
        private static  IAppLogger _appLogger;


        public StartConfiguration()
        {
           
        }

        public static async Task<IHost> InitializeApplicationAsync(string[] args)
        {
            _appLogger = new AppLogger();
           
            _appLogger.Info($"Starting {nameof(InitializeApplicationAsync)}");

            var host = CreateHostBuilder(args).Build();


            var connectionString = host.Services.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection");
            var youtubeApiKey = host.Services.GetRequiredService<IConfiguration>()["YouTubeApiKey"];

            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MatchFilterDbContext> ();
                try
                {
                    await dbContext.Database.OpenConnectionAsync();
                     _appLogger.Info("Successfully connected to the database.");
                }
                catch (Exception ex)
                {
                     _appLogger.Info($"Failed to connect to the database: {ex.Message}");
                }
            }

            return host;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureServices((hostContext, services) =>
              {
                  services.AddDbContext<MatchFilterDbContext>(options =>
                  {
                      options.UseNpgsql(
                          hostContext.Configuration.GetConnectionString("DefaultConnection"),
                          b => b.MigrationsAssembly("LolMatchFilterNew.Infrastructure")
                      );
                      options.EnableSensitiveDataLogging();
                      options.LogTo(Console.WriteLine);
                  });

                  services.AddSingleton<IAppLogger, AppLogger>();
                  services.AddSingleton<ActivitySource>(new ActivitySource("LolMatchFilterNew"));
                  services.AddSingleton<ITeamNameHistoryFormatter, TeamNameHistoryFormatter>();
                  services.AddSingleton<ITeamNameValidator, TeamNameValidator>();
         

                  services.AddTransient<IYoutubeApi, YoutubeApi>();
                  services.AddTransient<ILeaguepediaDataFetcher, LeaguepediaDataFetcher>();
                  services.AddTransient<IApiHelper, ApiHelper>();
                  services.AddTransient<IYoutubeTitleMatcher, YoutubeTitleMatcher>();
                  services.AddTransient<IActivityService, ActivityService>();
                  services.AddTransient<ILeaguepediaQueryService, LeaguepediaQueryService>();
                  services.AddTransient<ILeaguepediaAPILimiter, LeaguepediaAPILimiter>();
                  services.AddTransient<ILeaguepediaApiMapper, LeaguepediaApiMapper>();
                  services.AddTransient<IImport_ScoreboardGamesRepository, Import_ScoreboardGamesRepository>();
                  services.AddTransient<IYoutubeMapper, YoutubeMapper>();
                  services.AddTransient<IYoutubeDataFetcher, YoutubeDataFetcher>();
                  services.AddTransient<IImport_YoutubeDataRepository, Import_YoutubeDataRepository>();
                  services.AddTransient<IYoutubeController, YoutubeController>();
                  services.AddTransient<IImport_ScoreboardGamesRepository, Import_ScoreboardGamesRepository>();
                  services.AddTransient<IImport_TeamRenameRepository, Import_TeamRenameRepository>();
                  services.AddTransient<IProcessed_TeamNameHistoryRepository, Processed_TeamNameHistoryRepository>();
                  services.AddTransient<IImport_TeamRedirectRepository, Import_TeamRedirectRepository>();
                  services.AddTransient<IImport_TeamnameRepository, Import_TeamnameRepository>();
                  services.AddTransient<IStoredSqlFunctionCaller, StoredSqlFunctionCaller>();
                  services.AddTransient<ITeamNameDTOBuilder, TeamnameDTOBuilder>();
                  services.AddTransient<IObjectLogger, ObjectLogger>();
                  services.AddTransient<IProcessed_YoutubeDataDTOBuilder, Processed_YoutubeDataDTOBuilder>();
                  services.AddTransient<IMatchComparisonResultBuilder, MatchComparisonResultBuilder>();
                  services.AddTransient<IMatchComparisonController, MatchComparisonController>();
                  services.AddTransient<IYoutubeTeamExtractor, YoutubeTeamExtractor>();
                  services.AddTransient<IScoreboardGamesTeamNameService, ScoreboardGamesTeamNameService>();




                  services.AddTransient<IMatchComparisonController, MatchComparisonController>();
        


                  services.AddScoped<IAPIControllers, APIControllers>();
                  services.AddScoped<IMatchFilterDbContext>(provider =>
                      provider.GetRequiredService<MatchFilterDbContext>());
                  services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));




                  services.AddHttpClient("YouTube", client =>
                  {
                      client.DefaultRequestHeaders.Add("User-Agent", "YourAppName/1.0");
                  });
              });

    }
}
