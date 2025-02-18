﻿
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_TeamRenameRepositories;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;

using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRenameEntities;
using Domain.Interfaces.InfrastructureInterfaces.IImport_TeamRenameRepositories;
using Npgsql;

namespace Infrastructure.Repositories.ImportRepositories.Import_TeamRenameRepositories
{
    public class Import_TeamRenameRepository : GenericRepository<Import_TeamRenameRepository>, IImport_TeamRenameRepository
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchFilterDbContext _matchFilterDbContext;

        public Import_TeamRenameRepository(IMatchFilterDbContext dbContext, IAppLogger appLogger)
          : base(dbContext as MatchFilterDbContext, appLogger)
        {
            _appLogger = appLogger;
            _matchFilterDbContext = dbContext;
        }

        // Retrieves all current team names by retrieving NewNames which do not appear in OriginalName
        public async Task<List<string>> GetCurrentTeamNamesAsync()
        {

            var teamRenames = await _matchFilterDbContext.Import_TeamRename.ToListAsync();
            var originalNames = teamRenames.Select(x => x.OriginalName).ToHashSet();

            return teamRenames
                .Select(x => x.NewName)
                .Where(newName => !originalNames.Contains(newName))
                .Distinct()
                .ToList();
        }


        public async Task<List<string>> TESTGet10CurrentTeamNamesAsync()
        {
            var teamRenames = await _matchFilterDbContext.Import_TeamRename.ToListAsync();

            var originalNames = teamRenames.Select(x => x.OriginalName).ToHashSet();

            return teamRenames
            .Select(x => x.NewName)
            .Where(newName => !originalNames.Contains(newName))
            .Distinct()
            .Take(10)
            .ToList();


        }


        // Retrieves all results from TeamReanmes which has kept the format of the data from Leaguepedia. 
        public async Task<List<Import_TeamRenameEntity>> GetAllTeamRenameValuesAsync()
        {
            return await _matchFilterDbContext.Import_TeamRename.ToListAsync();
        }

        public async Task<IEnumerable<Import_TeamRenameEntity>> GetTeamHistoryAsync(string teamName)
        {
            var query = "SELECT * FROM get_team_name_history(@teamName)";
            var parameter = new NpgsqlParameter("@teamName", teamName);
            return await _matchFilterDbContext.Import_TeamRename.FromSqlRaw(query, parameter).ToListAsync();
        }









    }
}
