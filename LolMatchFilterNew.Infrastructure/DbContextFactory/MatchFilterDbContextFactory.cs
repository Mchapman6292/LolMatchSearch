using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using LolMatchFilterNew.Infrastructure.DbContextFactory.MatchFilterDbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Infrastructure.DbContextFactory.LolMatchFilterDbContextFactory
{

    // Migrations run at design-time, before application starts & before dependency injection containers & config are available.
    // Provides context & options when applying migrations
    public class LolMatchFilterDbContextFactory : IDesignTimeDbContextFactory<MatchFilterDbContext>
    {
        public MatchFilterDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<MatchFilterDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseNpgsql(connectionString);

            return new MatchFilterDbContext(builder.Options);
        }
    }
}