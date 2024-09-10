using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IYoutubeApi;
using LolMatchFilterNew.Domain.ILeaguepediaApis;
using LolMatchFilterNew.Infrastructure.Logging.AppLoggers;
using LolMatchFilterNew.Domain;
using LolMatchFilterNew.Infastructure.HttpJsonServices;
using LolMatchFilterNew.Domain.Interfaces.IHttpJsonServices;
using LolMatchFilterNew.Domain.Apis;

namespace LolMatchFilterNew.Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using var activity = new Activity("Application Start");
            var services = ConfigureServices();

            using var serviceProvider = services.BuildServiceProvider();
            var leaguepediaApi = serviceProvider.GetRequiredService<ILeaguepediaApi>();
            var logger = serviceProvider.GetRequiredService<IAppLogger>();
            var youtubeApi = serviceProvider.GetRequiredService<IYoutubeApi>();
            var applogger = serviceProvider.GetRequiredService<IAppLogger>();


            try
            {
                await leaguepediaApi.GetCapsAhriMatchesAsync(activity);
                Console.WriteLine("GetCapsAhriMatchesAsync completed successfully.");

                string videoTitle = "G2 vs MDK Highlights Game 2 | LEC Season Finals 2024 Upper Round 1 | G2 Esports vs MAD Lions KOI G2";
                string gameId = "LEC/2024 Season/Season Finals_Round 1_2_2";
                await youtubeApi.GetAndDocumentVideoDataAsync(videoTitle, gameId, activity);
                Console.WriteLine("YouTube video data retrieval and documentation completed successfully.");
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred while calling GetCapsAhriMatchesAsync: {ex.Message}");
                Console.WriteLine($"An error occurred while calling GetCapsAhriMatchesAsync: {ex.Message}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables();

            IConfiguration configuration = configurationBuilder.Build();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<IAppLogger, AppLogger>();
            services.AddTransient<IYoutubeApi, YoutubeApi>();
            services.AddTransient<ILeaguepediaApi, LeaguepediaApi>();
            services.AddTransient<IHttpJsonService, HttpJsonService>();

            services.AddHttpClient("YouTube", client =>
            {
                client.DefaultRequestHeaders.Add("User-Agent", "YourAppName/1.0");
            });

            return services;
        }
    }
}