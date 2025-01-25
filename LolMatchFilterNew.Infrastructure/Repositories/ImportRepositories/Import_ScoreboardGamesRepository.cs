using Domain.Interfaces.ApplicationInterfaces.IDTOBuilders.IWesternMatchDTOFactories;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_ScoreboardGamesEntities;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_ScoreboardGamesRepositories;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.ImportRepositories.Import_ScoreboardGamesRepositories
{
    public class Import_ScoreboardGamesRepository : GenericRepository<Import_ScoreboardGamesEntity>, IImport_ScoreboardGamesRepository
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchFilterDbContext _matchFilterDbContext;
        private readonly IWesternMatchDTOFactory _westernMatchDTOFactory;

        public Import_ScoreboardGamesRepository(IMatchFilterDbContext dbContext, IAppLogger appLogger, IWesternMatchDTOFactory westernMatchDtoFactory)
            : base(dbContext as MatchFilterDbContext, appLogger)
        {
            _appLogger = appLogger;
            _matchFilterDbContext = dbContext;
            _westernMatchDTOFactory = westernMatchDtoFactory;
        }

        public async Task<int> BulkAddScoreboardGames(IEnumerable<Import_ScoreboardGamesEntity> matchDetails)
        {
            int totalCount = matchDetails.Count();
            _appLogger.Info($"Starting bulk add of {totalCount} Leaguepedia match details.");
            LogTrackedScoreboardGames();

            try
            {
                int processedCount = 0;

                foreach (var matchDetail in matchDetails)
                {
                    if (matchDetail.DateTime_utc != null && matchDetail.DateTime_utc.Value.Kind != DateTimeKind.Utc)
                    {
                        matchDetail.DateTime_utc = DateTime.SpecifyKind(matchDetail.DateTime_utc.Value, DateTimeKind.Utc);
                    }
                    _matchFilterDbContext.Import_ScoreboardGames.Add(matchDetail);
                    processedCount++;

                    // Used to log progress at regular intervals(every 20% or 500 items)
                    if (processedCount % Math.Max(totalCount / 5, 500) == 0)
                    {
                        _appLogger.Info($"Processed {processedCount} of {totalCount} entities.");
                        LogTrackedScoreboardGames();
                    }
                }

                _appLogger.Info($"Saving changes for {processedCount} entities...");
                int addedCount = await _matchFilterDbContext.SaveChangesAsync();
                _appLogger.Info($"Successfully added {addedCount} new matches out of {totalCount} processed.");
                LogTrackedScoreboardGames();

                return addedCount;
            }
            catch (DbUpdateException ex)
            {
                _appLogger.Error($"Failed to bulk add matches. Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    _appLogger.Error($"Inner exception: {ex.InnerException.Message}");
                }
                LogTrackedScoreboardGames();
                throw;
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Unexpected error during bulk add: {ex.Message}");
                LogTrackedScoreboardGames();
                throw;
            }
        }


        public void LogTrackedScoreboardGames()
        {
            var trackedEntities = _matchFilterDbContext.ChangeTracker.Entries<Import_ScoreboardGamesEntity>()
                .Select(e => new
                {
                    Key = e.Property(p => p.GameId).CurrentValue,
                    e.State
                }).ToList();

            _appLogger.Info($"Number of tracked Import_ScoreboardGamesEntity: {trackedEntities.Count}");
        }







    }
}
