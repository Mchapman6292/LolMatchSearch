using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRedirectEntities;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_TeamRedirectRepositories;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using LolMatchFilterNew.Infrastructure.Repositories.TeamRenameRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Infrastructure.Repositories.Import_TeamRedirectRepositories
{
    public class Import_TeamRedirectRepository : GenericRepository<Import_TeamRedirectRepository>, IImport_TeamRedirectRepository
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchFilterDbContext _matchFilterDbContext;

        public Import_TeamRedirectRepository(IMatchFilterDbContext dbContext, IAppLogger appLogger)
          : base(dbContext as MatchFilterDbContext, appLogger)
        {
            _appLogger = appLogger;
            _matchFilterDbContext = dbContext;
        }


        public async Task<IEnumerable<Import_TeamRedirectEntity>> GetAllTeamRedirects()
        {
            return await _matchFilterDbContext.Import_TeamRedirect.ToListAsync();
        }


    }
}
