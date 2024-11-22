using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IYoutubeMapper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Infrastructure.DataConversion.YoutubeMappers
{
    public class YoutubeMapper: IYoutubeMapper
    {
        private readonly IAppLogger _appLogger;
        private readonly IApiHelper _apiHelper;


        public YoutubeMapper(IAppLogger appLogger, IApiHelper apiHelper)
        {
            _appLogger = appLogger;
            _apiHelper = apiHelper;
        }

        public async Task<IEnumerable<YoutubeVideoEntity>> MapYoutubeToEntity(IEnumerable<JObject> videoData)
        {
            if (videoData == null || !videoData.Any())
            {
                _appLogger.Error($"Null or empty data for {nameof(videoData)}");
                throw new ArgumentNullException(nameof(MapYoutubeToEntity), "Input data cannot be null or empty.");
            }

            return await Task.Run(() =>
            {
                var results = new List<YoutubeVideoEntity>();
                int processedCount = 0;

                foreach (var video in videoData)
                {
                    processedCount++;
                    try
                    {
                        var entity = new YoutubeVideoEntity
                        {
                            YoutubeVideoId = _apiHelper.GetStringValue(video, "id"),
                            VideoTitle = _apiHelper.GetStringValue(video, "snippet.title"),
                            PublishedAt = _apiHelper.GetDateTimeFromJObject(video, "snippet.publishedAt"),
                            YoutubeResultHyperlink = $"https://www.youtube.com/watch?v={_apiHelper.GetStringValue(video, "id")}",
                            ThumbnailUrl = _apiHelper.GetStringValue(video, "snippet.thumbnails.default.url"),
                            LeaguepediaGameIdAndTitle = _apiHelper.GetStringValue(video, "leaguepediaGameIdAndTitle") 
                        };

                        results.Add(entity);
                    }
                    catch (Exception ex)
                    {
                        _appLogger.Error($"Error processing video data {processedCount}: {ex.Message}");
                        _appLogger.Error($"Raw data: {video}");
                    }
                }

                _appLogger.Info($"Deserialized {results.Count} entities out of {processedCount} processed.");
                return results;
            });
        }

        public async Task<IEnumerable<YoutubeVideoEntity>> MapYoutubeToEntityTesting(IEnumerable<JObject> videoData, int limit = 2)
        {
            if (videoData == null || !videoData.Any())
            {
                _appLogger.Error($"Null or empty data for {nameof(videoData)}");
                throw new ArgumentNullException(nameof(MapYoutubeToEntity), "Input data cannot be null or empty.");
            }

            return await Task.Run(() =>
            {
                var results = new List<YoutubeVideoEntity>();
                int processedCount = 0;
                int successCount = 0;

                foreach (var video in videoData)
                {
                    processedCount++;
                    try
                    {
                        var entity = new YoutubeVideoEntity
                        {
                            YoutubeVideoId = _apiHelper.GetStringValue(video, "id"),
                            VideoTitle = _apiHelper.GetStringValue(video, "snippet.title"),
                            PublishedAt = _apiHelper.GetDateTimeFromJObject(video, "snippet.publishedAt"),
                            YoutubeResultHyperlink = $"https://www.youtube.com/watch?v={_apiHelper.GetStringValue(video, "id")}",
                            ThumbnailUrl = _apiHelper.GetStringValue(video, "snippet.thumbnails.default.url"),
                            LeaguepediaGameIdAndTitle = _apiHelper.GetStringValue(video, "leaguepediaGameIdAndTitle")
                        };
                        results.Add(entity);
                        successCount++;

                        if (successCount >= limit)
                        {
                            _appLogger.Info($"Reached limit of {limit} successfully processed videos. Stopping processing.");
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        _appLogger.Error($"Error processing video data {processedCount}: {ex.Message}");
                        _appLogger.Error($"Raw data: {video}");
                    }
                }

                _appLogger.Info($"Deserialized {successCount} entities out of {processedCount} processed. Limit was set to {limit}.");
                return results;
            });
        }


    }
}
