using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Apis.YoutubeApi;
using LolMatchFilterNew.Domain.Interfaces.IYoutubeApi;
using LolMatchFilterNew.Domain.Interfaces.ILeaguepediaApis;
using LolMatchFilterNew.Infrastructure.Logging.AppLoggers;
using LolMatchFilterNew.Domain;
using LolMatchFilterNew.infrastructure.HttpJsonServices;
using LolMatchFilterNew.Domain.Interfaces.IHttpJsonServices;
using LolMatchFilterNew.Domain.Apis.LeaguepediaApis;
using LolMatchFilterNew.Domain.Helpers.ApiHelper;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.UnUsedYoutubeClass.YoutubeTitleMatcher;
using LolMatchFilterNew.Domain.Interfaces.IYoutubeTitleMatcher;
using LolMatchFilterNew.Infrastructure.Logging.ActivityService;
using LolMatchFilterNew.Domain.Interfaces.IActivityService;
using LolMatchFilterNew.Domain.Entities.ProPlayerEntities;
using LolMatchFilterNew.infrastructure.Repositories.GenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using Microsoft.Extensions.Hosting;
using LolMatchFilterNew.Infrastructure.DbContextFactory;
using Microsoft.EntityFrameworkCore;


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
             _appLogger.Info($"Connection String: {connectionString}");

            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<Infrastructure.DbContextFactory.MatchFilterDbContexts>();
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
             .ConfigureServices((Action<HostBuilderContext, IServiceCollection>)((hostContext, services) =>
             {
                 EntityFrameworkServiceCollectionExtensions.AddDbContext<Infrastructure.DbContextFactory.MatchFilterDbContexts>(services, (Action<DbContextOptionsBuilder>?)(options =>
                     options.UseNpgsql(
                         hostContext.Configuration.GetConnectionString("DefaultConnection"),
                         b => b.MigrationsAssembly("LolMatchFilterNew.Infrastructure")
                             ))
                 );


                 services.AddSingleton<IAppLogger, AppLogger>();
                 services.AddSingleton<ActivitySource>(new ActivitySource("LolMatchFilterNew"));

                 services.AddTransient<IYoutubeApi, YoutubeApi>();
                 services.AddTransient<ILeaguepediaApi, LeaguepediaApi>();
                 services.AddTransient<IHttpJsonService, HttpJsonService>();
                 services.AddTransient<IApiHelper, ApiHelper>();
                 services.AddTransient<IYoutubeTitleMatcher, YoutubeTitleMatcher>();
                 services.AddTransient<IActivityService, ActivityService>();
                 services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


                 services.AddHttpClient("YouTube", client =>
                 {
                     client.DefaultRequestHeaders.Add("User-Agent", "YourAppName/1.0");
                 });
             }));

    }
}
