using LolMatchFilterNew.Application.Configuration.StartConfiguration;
using Microsoft.Extensions.Hosting;
using LolMatchFilterNew.Domain.Apis.LeaguepediaApis;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.ILeaguepediaApis;
using Microsoft.Extensions.DependencyInjection;

namespace LolMatchFilterNew.Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = await StartConfiguration.InitializeApplicationAsync(args);

            using (var scope = host.Services.CreateScope())
            {
                var leaguepediaApi = scope.ServiceProvider.GetRequiredService<ILeaguepediaApi>();

                var results = await leaguepediaApi.FetchLeaguepediaMatchesForTestingAsync("LEC 2023 Summer Season", 300);


                var logger = scope.ServiceProvider.GetRequiredService<IAppLogger>();
                logger.Info($"Fetched {results.Count()} matches for testing.");
            }

            await host.RunAsync();
        }
    }
}