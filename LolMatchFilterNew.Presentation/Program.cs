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
using LolMatchFilterNew.Domain.UnUsedYoutubeClass.YoutubeTitleMatcher;
using LolMatchFilterNew.Domain.Interfaces.IYoutubeTitleMatcher;
using LolMatchFilterNew.Infrastructure.Logging.ActivityService;
using LolMatchFilterNew.Domain.Interfaces.IActivityService;
using LolMatchFilterNew.Domain.Entities.ProPlayerEntities;



using Microsoft.EntityFrameworkCore.Design;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Hosting;

using Activity = System.Diagnostics.Activity;
using Serilog;
using LolMatchFilterNew.Infrastructure.DbContextFactory;
using Microsoft.EntityFrameworkCore;

namespace LolMatchFilterNew.Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Log the connection string
            var connectionString = host.Services.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection");
            Console.WriteLine($"Connection String: {connectionString}");

            // Test database connection
            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<LolMatchFilterDbContext>();
                try
                {
                    await dbContext.Database.OpenConnectionAsync();
                    Console.WriteLine("Successfully connected to the database.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to connect to the database: {ex.Message}");
                }
            }

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var appLogger = services.GetRequiredService<IAppLogger>();
                var activitySource = services.GetRequiredService<ActivitySource>();
                var leaguepediaApi = services.GetRequiredService<ILeaguepediaApi>();
                var youtubeApi = services.GetRequiredService<IYoutubeApi>();

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
                        string youtubePlaylistId = "PLJwuLHutaYuLMHzkyblz2q0HlDd7otgJA";
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
            }

            await host.RunAsync();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<LolMatchFilterDbContext>(options =>
                        options.UseNpgsql(
                            hostContext.Configuration.GetConnectionString("DefaultConnection"),
                            b => b.MigrationsAssembly("LolMatchFilterNew.Infrastructure")
                                )
                    );


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
                });
    }
}