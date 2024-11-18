using LolMatchFilterNew.Application.TeamHistoryService.TeamHistoryLogics;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.ITeamHistoryLogic;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamRenameRepositories;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.Logging.AppLoggers;
using LolMatchFilterNew.Infrastructure.Repositories.TeamRenameRepositories;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Tests.TestLoggers;
using LolMatchFilterNew.Domain.Entities.TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ITeamNameHistoryFormatters;
using LolMatchFilterNew.Domain.Formatters.TeamNameHistoryFormatters;

namespace LolMatchFilterNew.Tests.TestServiceFactories
{
    public class TestServiceFactory
    {
        private readonly IHost _host;

        public TestServiceFactory()
        {
            _host = CreateTestHostBuilder(Array.Empty<string>()).Build();
        }

        private static IHostBuilder CreateTestHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var appSettingsPath = @"C:\Users\mchap\source\repos\MatchFilterNew\LolMatchFilterNew.Application";
                    config.SetBasePath(appSettingsPath)
                          .AddJsonFile("appsettings.json");
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
                    });

                    services.AddSingleton<TestLogger>();
                    services.AddSingleton<IAppLogger, AppLogger>();
                    services.AddTransient<ITeamRenameRepository, TeamRenameRepository>();
                    services.AddTransient<ITeamHistoryLogic, TeamHistoryLogic>();
                    services.AddTransient<ITeamNameHistoryFormatter, TeamNameHistoryFormatter>();
                    services.AddScoped<IMatchFilterDbContext>(provider =>
                        provider.GetRequiredService<MatchFilterDbContext>());
                    services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
                });

        public IAppLogger GetAppLogger() =>
            _host.Services.GetRequiredService<IAppLogger>();

        public ITeamHistoryLogic GetTeamHistoryLogic() =>
            _host.Services.GetRequiredService<ITeamHistoryLogic>();

        public TestLogger GetTestLogger() =>
         _host.Services.GetRequiredService<TestLogger>();

        public ITeamNameHistoryFormatter GetTeamNameHistoryFormatter() =>
            _host.Services.GetRequiredService<ITeamNameHistoryFormatter>();
    }
}