using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamRenameRepositories;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamHistoryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Entities.Processed_TeamNameHistoryEntities;
using Microsoft.EntityFrameworkCore;

namespace LolMatchFilterNew.Infrastructure.Repositories.Processed_TeamNameHistoryRepositories
{


    public class Processed_TeamNameHistoryRepository : GenericRepository<Processed_TeamNameHistoryEntity>, ITeamHistoryRepository
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchFilterDbContext _matchFilterDbContext;

        public Processed_TeamNameHistoryRepository(IMatchFilterDbContext dbContext, IAppLogger appLogger)
            : base(dbContext as MatchFilterDbContext, appLogger)
        {
            _appLogger = appLogger;
            _matchFilterDbContext = dbContext;
        }


    }
}