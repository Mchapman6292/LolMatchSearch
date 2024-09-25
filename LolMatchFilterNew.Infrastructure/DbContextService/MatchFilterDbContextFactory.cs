using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Infrastructure.DbContextService.LolMatchFilterDbContextFactory
{

    // Migrations run at design-time, before application starts & before dependency injection containers & config are available.
    // Provides context & options when applying migrations
    public class LolMatchFilterDbContextFactory : IDesignTimeDbContextFactory<MatchFilterDbContext.MatchFilterDbContext>
    {
        public MatchFilterDbContext.MatchFilterDbContext CreateDbContext(string[] args)
        {
            var appSettingsPath = @"C:\Users\mchap\source\repos\MatchFilterNew\LolMatchFilterNew.Application";

            var appSettingsFile = Path.Combine(appSettingsPath, "appsettings.json");

            if (!File.Exists(appSettingsFile))
            {
                throw new FileNotFoundException($"Configuration file not found: {appSettingsFile}");
            }

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(appSettingsPath)
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<MatchFilterDbContext.MatchFilterDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseNpgsql(connectionString);

            return new MatchFilterDbContext.MatchFilterDbContext(builder.Options);
        }
    }
}