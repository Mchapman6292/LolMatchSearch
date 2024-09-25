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

        public LeaguepediaMatchDetailRepository(IMatchFilterDbContext dbcontex, IAppLogger appLogger)
            : base(dbcontex as DbContext, appLogger)
        {
            _appLogger = appLogger;
            _matchFilterDbContext = dbcontex;
        }

        public async Task AddLeaguepediaMatchDetails(IEnumerable<LeaguepediaMatchDetailEntity> matchDetails)
        {
            int savedCount = 0;
            int failureCount = 0;




            await _matchFilterDbContext.SaveChangesAsync();
        }
    }
}
