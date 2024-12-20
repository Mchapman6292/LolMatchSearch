﻿
using Google.Apis.YouTube.v3.Data;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
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

        public async Task <Import_YoutubeDataEntity> MapToImport_YoutubeDataEntity(PlaylistItem item, string playlistTitle)
        {
            return new Import_YoutubeDataEntity
            {
                YoutubeVideoId = item.ContentDetails.VideoId,
                VideoTitle = item.Snippet.Title,
                PlaylistId = item.Snippet.PlaylistId,
                PlaylistTitle = playlistTitle,
                PublishedAt_utc = item.Snippet.PublishedAt ?? DateTime.UtcNow,
                YoutubeResultHyperlink = $"https://www.youtube.com/watch?v={item.ContentDetails.VideoId}",
                ThumbnailUrl = item.Snippet.Thumbnails.Default__?.Url
            };
        }

        public async Task<IEnumerable<Import_YoutubeDataEntity>> MapYoutubeToEntityTesting(IEnumerable<JObject> videoData, int limit = 2)
        {
            if (videoData == null || !videoData.Any())
            {
                _appLogger.Error($"Null or empty data for {nameof(videoData)}");
                throw new ArgumentNullException(nameof(MapToImport_YoutubeDataEntity), "Input data cannot be null or empty.");
            }
            return await Task.Run(() =>
            {
                var results = new List<Import_YoutubeDataEntity>();
                int processedCount = 0;
                int successCount = 0;
                foreach (var video in videoData)
                {
                    processedCount++;
                    try
                    {
                        var entity = new Import_YoutubeDataEntity
                        {
                            YoutubeVideoId = _apiHelper.GetNullableStringValue(video, "id"),
                            VideoTitle = _apiHelper.GetNullableStringValue(video, "snippet.title"),
                            PublishedAt_utc = _apiHelper.GetDateTimeFromJobject(video, "snippet.publishedAt"),
                            YoutubeResultHyperlink = $"https://www.youtube.com/watch?v={_apiHelper.GetNullableStringValue(video, "id")}",
                            ThumbnailUrl = _apiHelper.GetNullableStringValue(video, "snippet.thumbnails.default.url"),
                            PlaylistTitle = _apiHelper.GetNullableStringValue(video, "snippet.playlistTitle"),
                            PlaylistId = _apiHelper.GetNullableStringValue(video, "snippet.playlistId")
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
