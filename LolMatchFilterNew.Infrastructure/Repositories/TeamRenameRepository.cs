using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamRenameRepositories;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Infrastructure.Repositories.TeamRenameRepositories
{
    public class TeamRenameRepository : GenericRepository<TeamRenameRepository>, ITeamRenameRepository
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchFilterDbContext _matchFilterDbContext;

        public TeamRenameRepository(IMatchFilterDbContext dbContext, IAppLogger appLogger)
          : base(dbContext as MatchFilterDbContext, appLogger)
        {
            _appLogger = appLogger;
            _matchFilterDbContext = dbContext;
        }

        // Retrieves all current team names by retrieving NewNames which do not appear in OriginalName
        public async Task<List<string>> GetCurrentTeamNamesAsync()
        {
            var teamRenames = await _matchFilterDbContext.TeamRenames.ToListAsync();

            var originalNames = teamRenames.Select(x => x.OriginalName).ToHashSet();

            return teamRenames
                .Select(x => x.NewName)
                .Where(newName => !originalNames.Contains(newName))
                .Distinct()
                .ToList();
        }
    }
}
