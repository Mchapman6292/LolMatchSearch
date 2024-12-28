using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_TeamRenameRepositories;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_TeamNameHistoryEntities;
using Infrastructure.QueryBuilders.APIQueryBuilders;


namespace LolMatchFilterNew.Infrastructure.Repositories.Processed_TeamNameHistoryRepositories
{


    public class Processed_TeamNameHistoryRepository : GenericRepository<Processed_TeamNameHistoryEntity>, IProcessed_TeamNameHistoryRepository
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

            if(result == null) 
            {
                throw new Exception($"Unable to find NameHistory for {nameof(TESTGetTeamsByNameAsync)} for team {teamName}.");
            }

            return result;
            
        }





    }
}