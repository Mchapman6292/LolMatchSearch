using System;
using System.IO;
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
using LolMatchFilterNew.Infastructure.HttpJsonServices;
using LolMatchFilterNew.Domain.Interfaces.IHttpJsonServices;
using LolMatchFilterNew.Domain.Apis.LeaguepediaApis;
using LolMatchFilterNew.Domain.Helpers.ApiHelper;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.YoutubeVideoInfo.YoutubeTitleMatcher;
using LolMatchFilterNew.Domain.Interfaces.IYoutubeTitleMatcher;
using LolMatchFilterNew.Infrastructure.Logging.ActivityService;
using LolMatchFilterNew.Domain.Interfaces.IActivityService;

using Activity = System.Diagnostics.Activity;
using Serilog;

namespace LolMatchFilterNew.Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var services = ConfigureServices();
            using var serviceProvider = services.BuildServiceProvider();

            var appLogger = serviceProvider.GetRequiredService<IAppLogger>();
            var activitySource = serviceProvider.GetRequiredService<ActivitySource>();
            var leaguepediaApi = serviceProvider.GetRequiredService<ILeaguepediaApi>();
            var youtubeApi = serviceProvider.GetRequiredService<IYoutubeApi>();

            using var activityListener = new ActivityListener
            {
                ShouldListenTo = _ => true,
                Sample = (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllData,
                ActivityStarted = activity => appLogger.Info($"Activity started: {activity.DisplayName}"),
                ActivityStopped = activity => appLogger.Info($"Activity stopped: {activity.DisplayName}")
            };
            ActivitySource.AddActivityListener(activityListener);

            using (var activity = activitySource.StartActivity("Application Start"))
            {
                try
                {
                    await leaguepediaApi.GetCapsAhriMatchesAsync();
                    appLogger.Info("GetCapsAhriMatchesAsync completed successfully.");

                    List<string> teamNames = new List<string> { "G2", "MDK" };
                    string videoTitle = "G2 vs MDK Highlights Game 2 | LEC Season Finals 2024 Upper Round 1 | G2 Esports vs MAD Lions KOI G2";
                    string gameId = "G2 vs MDK Highlights Game 2 | LEC Season Finals 2024 Upper Round 1 | G2 Esports vs MAD Lions KOI G2";
                    await youtubeApi.GetAndDocumentVideoDataAsync(activity, gameId, videoTitle, teamNames);
                    appLogger.Info("YouTube video data retrieval and documentation completed successfully.");
                }
                catch (Exception ex)
                {
                    appLogger.Error($"An error occurred: {ex.Message}", ex);
                }
                finally
                {
                    Log.CloseAndFlush();
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables();

            IConfiguration configuration = configurationBuilder.Build();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<IAppLogger, AppLogger>();
            services.AddSingleton(new ActivitySource("LolMatchFilterNew"));


            services.AddTransient<IYoutubeApi, YoutubeApi>();
            services.AddTransient<ILeaguepediaApi, LeaguepediaApi>();
            services.AddTransient<IHttpJsonService, HttpJsonService>();
            services.AddTransient<IApiHelper, ApiHelper>();
            services.AddTransient<IYoutubeTitleMatcher, YoutubeTitleMatcher>();
            services.AddTransient<IActivityService, ActivityService>();

            services.AddHttpClient("YouTube", client =>
            {
                client.DefaultRequestHeaders.Add("User-Agent", "YourAppName/1.0");
            });

            return services;
        }
    }
}