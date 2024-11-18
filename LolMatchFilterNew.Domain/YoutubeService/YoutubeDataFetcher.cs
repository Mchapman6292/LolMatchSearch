using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IActivityService;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using LolMatchFilterNew.Domain.Helpers.ApiHelper;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.IYoutubeDataFetcher;
using Newtonsoft.Json.Linq;
using LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IYoutubeMapper;
using LolMatchFilterNew.Domain.Helpers.YoutubeIdHelpers;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Google;


namespace LolMatchFilterNew.Domain.YoutubeDataFetcher
{
    public class YoutubeDataFetcher : IYoutubeDataFetcher
    {
        private readonly string _apiKey;
        private readonly IAppLogger _appLogger;
        private readonly IActivityService _activityService;
        private readonly YouTubeService _youtubeService;
        private readonly IApiHelper _apiHelper;
        private readonly IYoutubeMapper _youtubeMapper;
        private const int MaxResultsPerPage = 50;
        private const string RequiredPlaylistParts = "snippet,contentDetails";
        private const string RequiredChannelParts = "snippet";
        public Dictionary<string, string> YoutubeplaylistNames = new Dictionary<string, string>();


        public YoutubeDataFetcher(IConfiguration configuration, IAppLogger appLogger, IActivityService activityService, IApiHelper apiHelper, IYoutubeMapper youtubeMapper)
        {
            _apiKey = configuration["YouTubeApiKey"];
            _appLogger = appLogger;
            _activityService = activityService;
            _apiHelper = apiHelper;
            _youtubeMapper = youtubeMapper;
            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _apiKey,
                ApplicationName = "LolMatchFilter"
            });
        }


        public async Task<IEnumerable<YoutubeVideoEntity>> GetVideosFromChannel(string channelId, int? maxResults = null)
        {
            _appLogger.Info($"Starting {nameof(GetVideosFromChannel)}");
            var playlists = await GetChannelPlaylists(channelId);
            var allVideos = new List<YoutubeVideoEntity>();

            foreach (var playlist in playlists)
            {
                try
                {
                    var videos = await GetVideosFromPlaylist(playlist.Key, maxResults);
                    allVideos.AddRange(videos);
                }
                catch (Exception ex)
                {
                    _appLogger.Error($"Failed to fetch videos from playlist {playlist.Key}: {ex.Message}");
                    continue;
                }
            }

            return allVideos;
        }

        public async Task<IEnumerable<YoutubeVideoEntity>> GetVideosFromPlaylist(string playlistId, int? maxResults = null)
        {
            var videos = new List<YoutubeVideoEntity>();
            var pageToken = string.Empty;
            var processedItems = 0;

            do
            {
                try
                {
                    var request = _youtubeService.PlaylistItems.List(RequiredPlaylistParts);
                    request.PlaylistId = playlistId;
                    request.MaxResults = MaxResultsPerPage;
                    request.PageToken = pageToken;

                    var response = await request.ExecuteAsync();

                    foreach (var item in response.Items)
                    {
                        videos.Add(MapPlaylistItemToEntity(item));
                        processedItems++;

                        if (maxResults.HasValue && processedItems >= maxResults)
                        {
                            return videos;
                        }
                    }

                    pageToken = response.NextPageToken;
                }
                catch (Exception ex)
                {
                    _appLogger.Error($"Error fetching playlist items: {ex.Message}");
                    throw;
                }
            }
            while (!string.IsNullOrEmpty(pageToken));

            return videos;
        }

        public async Task<Dictionary<string, string>> GetChannelPlaylists(string channelId)
      

        public async Task<string> GetChannelIdFromInput(string input)
        {
            try
            {
                if (!input.StartsWith("@"))
                {
                    var videoRequest = _youtubeService.Videos.List("snippet");
                    videoRequest.Id = input;
                    var response = await videoRequest.ExecuteAsync();
                    return response.Items.FirstOrDefault()?.Snippet.ChannelId;
                }
                else
                {
                    var searchRequest = _youtubeService.Search.List("snippet");
                    searchRequest.Q = input.TrimStart('@');
                    searchRequest.Type = "channel";
                    searchRequest.MaxResults = 1;
                    var response = await searchRequest.ExecuteAsync();
                    return response.Items.FirstOrDefault()?.Snippet.ChannelId;
                }
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error retrieving channel ID: {ex.Message}");
                throw;
            }
        }

        private YoutubeVideoEntity MapPlaylistItemToEntity(PlaylistItem item)
        {
            return new YoutubeVideoEntity
            {
                YoutubeVideoId = item.ContentDetails.VideoId,
                Title = item.Snippet.Title,
                PlaylistName = item.Snippet.PlaylistId,
                PublishedAt = item.Snippet.PublishedAt ?? DateTime.UtcNow,
                YoutubeResultHyperlink = $"https://www.youtube.com/watch?v={item.ContentDetails.VideoId}",
                ThumbnailUrl = item.Snippet.Thumbnails.Default__?.Url
            };
        }
    }
}