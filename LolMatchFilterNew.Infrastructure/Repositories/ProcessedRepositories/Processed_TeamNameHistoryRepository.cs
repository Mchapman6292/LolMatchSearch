using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_TeamRenameRepositories;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using Domain.DTOs.TeamNameHistoryDTOs;
using Infrastructure.QueryBuilders.APIQueryBuilders;
using Infrastructure.Repositories.ImportRepositories.Import_TeamRenameRepositories;


namespace Infrastructure.Repositories.ProcessedRepositories
{


    public class Processed_TeamNameHistoryRepository : GenericRepository<Import_TeamRenameRepository>, IProcessed_TeamNameHistoryRepository
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchFilterDbContext _matchFilterDbContext;

        public Processed_TeamNameHistoryRepository(IMatchFilterDbContext dbContext, IAppLogger appLogger)
            : base(dbContext as MatchFilterDbContext, appLogger)
        {
            _appLogger = appLogger;
            _matchFilterDbContext = dbContext;

        }


        public async Task<List<string>> TESTGetTeamsByNameAsync(string teamName)
        {
            APIQueryBuilder queryBuilder = new APIQueryBuilder(_matchFilterDbContext);

            List<string>? result = await queryBuilder
                .WithTeam(teamName)
                .SelectNameHistory()
                .FirstOrDefaultAsync();

            if (result == null)
            {
                throw new Exception($"Unable to find NameHistory for {nameof(TESTGetTeamsByNameAsync)} for team {teamName}.");
            }

            return result;

        }





    }
}