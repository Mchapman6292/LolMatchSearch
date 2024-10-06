using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.infrastructure.Repositories.GenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaMatchDetailRepository;
using Microsoft.EntityFrameworkCore;
using LolMatchFilterNew.Infrastructure.DbContextService.LolMatchFilterDbContextFactory;

namespace LolMatchFilterNew.Infrastructure.Repositories.LeaguepediaMatchDetailRepository
{
    public class LeaguepediaMatchDetailRepository : GenericRepository<LeaguepediaMatchDetailEntity>, ILeaguepediaMatchDetailRepository
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchFilterDbContext _matchFilterDbContext;

        public LeaguepediaMatchDetailRepository(IMatchFilterDbContext dbContext, IAppLogger appLogger)
            : base(dbContext as DbContext, appLogger)
        {
            _appLogger = appLogger;
            _matchFilterDbContext = dbContext;
        }

        public async Task<int> BulkAddLeaguepediaMatchDetails(IEnumerable<LeaguepediaMatchDetailEntity> matchDetails)
        {
            try
            {
                int failedCount = 0;


                _matchFilterDbContext.LeaguepediaMatchDetails.AddRange(matchDetails);

                int addedCount = await _matchFilterDbContext.SaveChangesAsync();

                _appLogger.Info($"Successfully added {addedCount} new matches.");

                return addedCount;
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Failed to bulk add matches. Error: {ex.Message}");
                throw;
            }
        }






   
    }
}

