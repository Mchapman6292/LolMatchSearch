using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IMatchFilterDbContext;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IYoutubeVideoRepository;
using LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Infrastructure.Repositories.YoutubeVideoRepository
{
    public class YoutubeVideoRepository : GenericRepository<YoutubeVideoRepository>, IYoutubeVideoRepository
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchFilterDbContext _matchFilterDbContext;

        public YoutubeVideoRepository(IMatchFilterDbContext dbContext, IAppLogger appLogger)
           : base(dbContext as DbContext, appLogger)
        {
            _appLogger = appLogger;
            _matchFilterDbContext = dbContext;
        }

        public async Task<int> BulkaddYoutubeDetails(IEnumerable<YoutubeVideoEntity> youtubeVideoDetails)
        {
            int totalCount = youtubeVideoDetails.Count();
            _appLogger.Info($"Starting bulk add of {totalCount} YouTube video details.");
            LogTrackedYoutubeEntities();
            try
            {
                int processedCount = 0;
                foreach (var videoDetail in youtubeVideoDetails)
                {
                    if (videoDetail.PublishedAt.Kind != DateTimeKind.Utc)
                    {
                        videoDetail.PublishedAt = DateTime.SpecifyKind(videoDetail.PublishedAt, DateTimeKind.Utc);
                    }
                    _matchFilterDbContext.YoutubeVideoResults.Add(videoDetail);
                    processedCount++;
                    if (processedCount % Math.Max(totalCount / 5, 500) == 0)
                    {
                        _appLogger.Info($"Processed {processedCount} of {totalCount} entities.");
                        LogTrackedYoutubeEntities();
                    }
                }
                _appLogger.Info($"Saving changes for {processedCount} entities...");
                int addedCount = await _matchFilterDbContext.SaveChangesAsync();
                _appLogger.Info($"Successfully added {addedCount} new YouTube videos out of {totalCount} processed.");
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




        public void LogTrackedYoutubeEntities()
        {
            var trackedEntities = _matchFilterDbContext.ChangeTracker.Entries<LeaguepediaMatchDetailEntity>()
                .Select(e => new
                {
                    Key = e.Property(p => p.LeaguepediaGameIdAndTitle).CurrentValue,
                    State = e.State
                }).ToList();

            _appLogger.Info($"Number of tracked LeaguepediaMatchDetailEntity: {trackedEntities.Count}");
        }

    }
}
