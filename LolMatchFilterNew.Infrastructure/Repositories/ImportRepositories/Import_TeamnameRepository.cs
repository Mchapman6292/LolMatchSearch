using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using Npgsql;

using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.InfrastructureInterfaces.IImport_TeamnameRepositories;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_Teamnames;

namespace Infrastructure.Repositories.ImportRepositories.Import_TeamnameRepositories
{
    public class Import_TeamnameRepository : GenericRepository<Import_TeamnameRepository>, IImport_TeamnameRepository
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchFilterDbContext _matchFilterDbContext;

        public Import_TeamnameRepository(IAppLogger appLogger, IMatchFilterDbContext matchFilterDbContext)
                      : base(matchFilterDbContext as MatchFilterDbContext, appLogger)
        {
            _appLogger = appLogger;
            _matchFilterDbContext = matchFilterDbContext;
        }



        public async Task<List<Import_TeamnameEntity>> GetAllTeamnamesAsync()
        {
            return await _matchFilterDbContext.Import_Teamname.ToListAsync();
        }




    }
}
