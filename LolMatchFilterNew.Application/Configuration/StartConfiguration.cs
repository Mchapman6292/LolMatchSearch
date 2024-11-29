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
using LolMatchFilterNew.Domain;
using LolMatchFilterNew.Domain.Apis.LeaguepediaDataFetcher;
using LolMatchFilterNew.Domain.Helpers.ApiHelper;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.UnUsedYoutubeClass.YoutubeTitleMatcher;
using LolMatchFilterNew.Domain.Interfaces.IYoutubeTitleMatcher;
using LolMatchFilterNew.Infrastructure.Logging.ActivityService;
using LolMatchFilterNew.Domain.Interfaces.IActivityService;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using LolMatchFilterNew.Infrastructure.Repositories.Import_ScoreboardGamesRepositories;
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
using LolMatchFilterNew.Domain.YoutubeDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.IYoutubeDataFetcher;
using LolMatchFilterNew.Infrastructure.Repositories.Import_YoutubeDataRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IYoutubeVideoRepository;
using LolMatchFilterNew.Application.Controllers.YoutubeControllers;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IYoutubeController;
using LolMatchFilterNew.Infrastructure.DataConversion.TeamRenameToHistoryMappers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamRenameToHistoryMappers;
using LolMatchFilterNew.Application.TeamHistoryService.TeamHistoryLogics;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.ITeamHistoryLogic;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ITeamNameHistoryFormatters;
using LolMatchFilterNew.Domain.Formatters.TeamNameHistoryFormatters;
using LolMatchFilterNew.Application.MatchPairingService.MatchServiceControllers;






using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Domain.YoutubeService;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces;
using LolMatchFilterNew.Application.Controllers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_TeamRenameRepositories;
using LolMatchFilterNew.Infrastructure.Repositories.TeamRenameRepositories;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchServiceControllers;
using LolMatchFilterNew.Infrastructure.Repositories.Processed_TeamNameHistoryRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_TeamRedirectRepositories;
using LolMatchFilterNew.Infrastructure.Repositories.Import_TeamRedirectRepositories;



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
                  services.AddTransient<IYoutubeVideoRepository, Import_YoutubeDataRepository>();
                  services.AddTransient<IYoutubeController, YoutubeController>();
                  services.AddTransient<IImport_ScoreboardGamesRepository, Import_ScoreboardGamesRepository>();
                  services.AddTransient<IImport_TeamRenameRepository, Import_TeamRenameRepository>();
                  services.AddTransient<ITeamRenameToHistoryMapper, TeamRenameToHistoryMapper>();
                  services.AddTransient<ITeamHistoryLogic, TeamHistoryLogic>();
                  services.AddTransient<IProcessed_TeamNameHistoryRepository, Processed_TeamNameHistoryRepository>();
                  services.AddTransient<IImport_TeamRedirectRepository, Import_TeamRedirectRepository>();
          
                  services.AddTransient<IMatchServiceController, MatchServiceController>();
        


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
