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
using LolMatchFilterNew.Domain.Entities.ProPlayerEntities;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryService;
using LolMatchFilterNew.Application.QueryBuilders.LeaguepediaQueryService;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.ApiLimiters.LeaguepediaAPILimiter;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaAPILimiter;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.ILeaguepediaIDGenerators;
using LolMatchFilterNew.Application.IdGenerators.LeaguepediaIDGenerators;
using LolMatchFilterNew.Infrastructure.DataConversion.LeaguepediaApiMappers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaApiMappers;
using LolMatchFilterNew.Application.LeaguepediaControllers;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.ILeaguepediaControllers;
using LolMatchFilterNew.Infrastructure.DataConversion.YoutubeMappers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IYoutubeMapper;
using LolMatchFilterNew.Domain.YoutubeDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.IYoutubeDataFetcher;
using LolMatchFilterNew.Infrastructure.Repositories.YoutubeVideoRepository;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IYoutubeVideoRepository;





using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaMatchDetailRepository;
using LolMatchFilterNew.Infrastructure.Repositories.LeaguepediaMatchDetailRepository;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Domain.YoutubeService;



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

                  services.AddTransient<IYoutubeApi, YoutubeApi>();
                  services.AddTransient<ILeaguepediaDataFetcher, LeaguepediaDataFetcher>();
                  services.AddTransient<IApiHelper, ApiHelper>();
                  services.AddTransient<IYoutubeTitleMatcher, YoutubeTitleMatcher>();
                  services.AddTransient<IActivityService, ActivityService>();
                  services.AddTransient<ILeaguepediaQueryService, LeaguepediaQueryService>();
                  services.AddTransient<ILeaguepediaAPILimiter, LeaguepediaAPILimiter>();
                  services.AddTransient<ILeaguepediaApiMapper, LeaguepediaApiMapper>();
                  services.AddTransient<ILeaguepediaIDGenerator, LeaguepediaIDGenerator>();
                  services.AddTransient<ILeaguepediaMatchDetailRepository, LeaguepediaMatchDetailRepository>();
                  services.AddTransient<ILeaguepediaApiMapper, LeaguepediaApiMapper>();
                  services.AddTransient<ILeaguepediaController, LeaguepediaController>();
                  services.AddTransient<IYoutubeMapper, YoutubeMapper>();
                  services.AddTransient<IYoutubeDataFetcher, YoutubeDataFetcher>();
                  services.AddTransient<IYoutubeVideoRepository, YoutubeVideoRepository>();

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
