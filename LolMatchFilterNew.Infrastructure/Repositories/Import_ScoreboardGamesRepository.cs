using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_ScoreboardGamesRepositories;
using Microsoft.EntityFrameworkCore;
using LolMatchFilterNew.Infrastructure.DbContextService.LolMatchFilterDbContextFactory;
using System.Runtime.CompilerServices;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_ScoreboardGamesEntities;


namespace LolMatchFilterNew.Infrastructure.Repositories.Import_ScoreboardGamesRepositories
{
    public class Import_ScoreboardGamesRepository : GenericRepository<Import_ScoreboardGamesEntity>, IImport_ScoreboardGamesRepository
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchFilterDbContext _matchFilterDbContext;

        public Import_ScoreboardGamesRepository(IMatchFilterDbContext dbContext, IAppLogger appLogger)
            : base(dbContext as MatchFilterDbContext, appLogger)
        {
            _appLogger = appLogger;
            _matchFilterDbContext = dbContext;
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
                    if (matchDetail.DateTime_utc.Kind != DateTimeKind.Utc)
                    {
                        matchDetail.DateTime_utc = DateTime.SpecifyKind(matchDetail.DateTime_utc, DateTimeKind.Utc);
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
                    State = e.State
                }).ToList();

            _appLogger.Info($"Number of tracked Import_ScoreboardGamesEntity: {trackedEntities.Count}");
        }

        // Remove for generic?

        public async Task<int> DeleteAllScoreboardGames()
        {
            try
            {
                var allRecords = await _matchFilterDbContext.Import_ScoreboardGames.ToListAsync();
                int count = allRecords.Count;

                _matchFilterDbContext.Import_ScoreboardGames.RemoveRange(allRecords);
                await _matchFilterDbContext.SaveChangesAsync();

                _appLogger.Info($"Successfully deleted {count} records from LeaguepediaMatchDetails.");
                return count;
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error occurred while deleting records: {ex.Message}");
                throw;
            }
        }
    }
}