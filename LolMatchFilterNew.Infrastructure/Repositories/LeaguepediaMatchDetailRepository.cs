using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.infrastructure.Repositories.GenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaMatchDetailRepository;
using Microsoft.EntityFrameworkCore;
using LolMatchFilterNew.Infrastructure.DbContextService.LolMatchFilterDbContextFactory;
using System.Runtime.CompilerServices;

namespace LolMatchFilterNew.Infrastructure.Repositories.LeaguepediaMatchDetailRepository
{
    public class LeaguepediaMatchDetailRepository : GenericRepository<LeaguepediaMatchDetailEntity>, ILeaguepediaMatchDetailRepository
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchFilterDbContext _matchFilterDbContext;

        public LeaguepediaMatchDetailRepository(IMatchFilterDbContext dbContext, IAppLogger appLogger)
            : base(dbContext as DbContext, appLogger)
        {
            _appLogger = appLogger;
            _matchFilterDbContext = dbContext;
        }

        public async Task<int> BulkAddLeaguepediaMatchDetails(IEnumerable<LeaguepediaMatchDetailEntity> matchDetails)
        {
            try
            {
                const int testLimit = 10;
                int addedCount = 0;
                int processedCount = 0;

                _appLogger.Info("Starting bulk add operation...");
                LogTrackedEntities();

                foreach (var matchDetail in matchDetails)
                {
                    if (processedCount >= testLimit)
                    {
                        _appLogger.Info($"Reached test limit of {testLimit} entities. Stopping processing.");
                        break;
                    }

                    if (matchDetail.DateTimeUTC.Kind != DateTimeKind.Utc)
                    {
                        matchDetail.DateTimeUTC = DateTime.SpecifyKind(matchDetail.DateTimeUTC, DateTimeKind.Utc);
                    }

                    _appLogger.Info($"Adding entity: GameId={matchDetail.LeaguepediaGameIdAndTitle}, DateTime={matchDetail.DateTimeUTC}");
                    _matchFilterDbContext.LeaguepediaMatchDetails.Add(matchDetail);
                    processedCount++;

                    if (processedCount % 5 == 0)
                    {
                        _appLogger.Info($"Added {processedCount} entities so far.");
                        LogTrackedEntities();
                    }
                }

                _appLogger.Info($"Saving changes for {processedCount} entities...");
                addedCount = await _matchFilterDbContext.SaveChangesAsync();

                _appLogger.Info($"Successfully added {addedCount} new matches out of {processedCount} processed.");
                LogTrackedEntities();

                return addedCount;
            }
            catch (DbUpdateException ex)
            {
                _appLogger.Error($"Failed to bulk add matches. Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    _appLogger.Error($"Inner exception: {ex.InnerException.Message}");
                }
                LogTrackedEntities();
                throw;
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Unexpected error during bulk add: {ex.Message}");
                LogTrackedEntities();
                throw;
            }
        }


        public void LogTrackedEntities()
        {
            var trackedEntities = _matchFilterDbContext.ChangeTracker.Entries<LeaguepediaMatchDetailEntity>()
                .Select(e => new
                {
                    Key = e.Property(p => p.LeaguepediaGameIdAndTitle).CurrentValue,
                    State = e.State
                }).ToList();

            _appLogger.Info($"Number of tracked LeaguepediaMatchDetailEntity: {trackedEntities.Count}");
            foreach (var entity in trackedEntities)
            {
                _appLogger.Info($"Tracked entity: Key = {entity.Key}, State = {entity.State}");
            }
        }
    }
}