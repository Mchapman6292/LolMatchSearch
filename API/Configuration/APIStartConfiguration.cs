using LolMatchFilterNew.Application.Configuration.StartConfiguration;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.Logging.AppLoggers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using API.Configuration.APIStartups;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_ScoreboardGamesRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_TeamRedirectRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_TeamRenameRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;
using LolMatchFilterNew.Infrastructure.Repositories.Import_ScoreboardGamesRepositories;
using LolMatchFilterNew.Infrastructure.Repositories.Import_TeamRedirectRepositories;
using LolMatchFilterNew.Infrastructure.Repositories.Import_YoutubeDataRepositories;
using LolMatchFilterNew.Infrastructure.Repositories.TeamRenameRepositories;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_TeamNameHistoryEntities;
using LolMatchFilterNew.Infrastructure.Repositories.Processed_TeamNameHistoryRepositories;


namespace API.Configuration.APIStartConfigurations
{

    public static class APIStartConfiguration
    {

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<APIStartup>();
                })
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

                    services.AddControllers();
                    services.AddEndpointsApiExplorer();
                    services.AddSwaggerGen();


                    services.AddSingleton<IAppLogger, AppLogger>();

                    services.AddScoped<IMatchFilterDbContext>(provider => provider.GetRequiredService<MatchFilterDbContext>());
                    services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

                    services.AddTransient<IProcessed_TeamNameHistoryRepository, Processed_TeamNameHistoryRepository>();

                });
  
    }
}
