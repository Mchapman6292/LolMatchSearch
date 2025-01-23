using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamsTableEntities;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using Domain.Interfaces.InfrastructureInterfaces.IImportRepositories.IImport_TeamsRepositories;
using Microsoft.EntityFrameworkCore;
using Domain.DTOs.ImportDTOs.Import_TeamsTableDTOs;

namespace Infrastructure.Repositories.ImportRepositories.Import_TeamsRepositories
{

    public class Import_TeamsRepository : GenericRepository<Import_TeamsTableEntity>, IImport_TeamsRepository
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchFilterDbContext _matchFilterDbContext;

        public Import_TeamsRepository(IMatchFilterDbContext dbContext,IAppLogger appLogger)
            : base(dbContext as MatchFilterDbContext, appLogger)
        {
            _appLogger = appLogger;
            _matchFilterDbContext = dbContext;
        }


        public async Task<List<Import_TeamsTableEntity>> JoinTeamsNameOnTeamNameLongName()
        {
            return await _matchFilterDbContext.Import_TeamsTable
                   .Join(
                       _matchFilterDbContext.Import_Teamname,
                       team => team.Name,
                       teamName => teamName.Longname,
                       (team, teamName) => team)
                   .AsNoTracking()
                   .ToListAsync();
        }


        public async Task<List<Import_TeamsTableDTO>> GetBasicTeamInfo(List<string> teamLongNames)
        {
            return await _matchFilterDbContext.Import_TeamsTable
                .Join(
                _matchFilterDbContext.Import_Teamname,
                team => team

        }

    }
}
