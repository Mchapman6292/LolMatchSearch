using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;
using LolMatchFilterNew.Infrastructure.DbContextService.MatchFilterDbContext;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_ScoreboardGamesEntities;
using NodaTime;
using NodaTime.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ImportRepositories.Import_YoutubeDataRepositories
{
    public class Import_YoutubeDataRepository : GenericRepository<Import_YoutubeDataRepository>, IImport_YoutubeDataRepository
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchFilterDbContext _matchFilterDbContext;

        public Import_YoutubeDataRepository(IMatchFilterDbContext dbContext, IAppLogger appLogger)
           : base(dbContext as MatchFilterDbContext, appLogger)
        {
            _appLogger = appLogger;
            _matchFilterDbContext = dbContext;
        }

        public async Task<int> BulkaddYoutubeDetails(IEnumerable<Import_YoutubeDataEntity> youtubeVideoDetails)
        {
            var distinctVideos = youtubeVideoDetails
                .GroupBy(x => x.YoutubeVideoId)
                .Select(g => g.First())
                .ToList();

            int totalCount = youtubeVideoDetails.Count();
            int distinctCount = distinctVideos.Count();

            _appLogger.Info($"Starting bulk add. Total videos: {totalCount}, Distinct videos: {distinctCount}");
            if (totalCount != distinctCount)
            {
                _appLogger.Info($"Found {totalCount - distinctCount} duplicate videos in incoming collection");
            }

            LogTrackedYoutubeEntities();

            try
            {
                var existingIds = await GetIdsOfSavedVideos();
                var newVideos = distinctVideos.Where(v => !existingIds.Contains(v.YoutubeVideoId)).ToList();

                _appLogger.Info($"Found {distinctCount - newVideos.Count} existing videos. Processing {newVideos.Count} new videos.");

                if (!newVideos.Any())
                {
                    _appLogger.Info("No new videos to add.");
                    return 0;
                }

                int processedCount = 0;
                foreach (var videoDetail in newVideos)
                {
                    if (videoDetail.PublishedAt_utc.HasValue && videoDetail.PublishedAt_utc.Value.Kind != DateTimeKind.Utc)
                    {
                        videoDetail.PublishedAt_utc = DateTime.SpecifyKind(videoDetail.PublishedAt_utc.Value, DateTimeKind.Utc);
                    }
                    _matchFilterDbContext.Import_YoutubeData.Add(videoDetail);
                    processedCount++;

                    if (processedCount % Math.Max(newVideos.Count / 5, 500) == 0)
                    {
                        _appLogger.Info($"Processed {processedCount} of {newVideos.Count} entities.");
                        LogTrackedYoutubeEntities();
                    }
                }

                _appLogger.Info($"Saving changes for {processedCount} entities...");
                int addedCount = await _matchFilterDbContext.SaveChangesAsync();
                _appLogger.Info($"Successfully added {addedCount} new YouTube videos out of {totalCount} total videos processed.");
                LogTrackedYoutubeEntities();
                return addedCount;
            }
            catch (DbUpdateException ex)
            {
                _appLogger.Error($"Failed to bulk add YouTube videos. Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    _appLogger.Error($"Inner exception: {ex.InnerException.Message}");
                }
                LogTrackedYoutubeEntities();
                throw;
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Unexpected error during bulk add of YouTube videos: {ex.Message}");
                LogTrackedYoutubeEntities();
                throw;
            }
        }

        public async Task<List<string>> GetIdsOfSavedVideos()
        {
            var existingIds = await _matchFilterDbContext.Import_YoutubeData
                .Select(e => e.YoutubeVideoId)
                .ToListAsync();

            _appLogger.Info($"Found {existingIds.Count} existing video IDs in database");
            return existingIds;
        }

        public void LogTrackedYoutubeEntities()
        {
            var trackedYoutubeEntities = _matchFilterDbContext.ChangeTracker
                .Entries<Import_YoutubeDataEntity>()
                .Select(e => new
                {
                    Key = e.Property(p => p.YoutubeVideoId).CurrentValue,
                    e.State
                }).ToList();

            var trackedLeaguepediaEntities = _matchFilterDbContext.ChangeTracker
                .Entries<Import_ScoreboardGamesEntity>()
                .Select(e => new
                {
                    Key = e.Property(p => p.GameId).CurrentValue,
                    e.State
                }).ToList();

            _appLogger.Info($"Number of tracked YouTube entities: {trackedYoutubeEntities.Count}");
            _appLogger.Info($"Number of tracked Leaguepedia entities: {trackedLeaguepediaEntities.Count}");
        }


        public async Task<int> DeleteAllImport_YoutubeData()
        {
            var allRecords = _matchFilterDbContext.Import_YoutubeData.ToList();
            _matchFilterDbContext.Import_YoutubeData.RemoveRange(allRecords);
            return await _matchFilterDbContext.SaveChangesAsync();
        }

    }
}
