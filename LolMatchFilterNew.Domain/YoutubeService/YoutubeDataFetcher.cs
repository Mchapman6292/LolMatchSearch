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
            var nextPageToken = "";
            var processedItems = 0;
            try
            {
                do
                {
                    var request = _youtubeService.PlaylistItems.List("snippet,contentDetails");
                    request.PlaylistId = playlistId;
                    request.MaxResults = 50;
                    request.PageToken = nextPageToken;
                    var response = await request.ExecuteAsync();
                    foreach (var item in response.Items)
                    {
                        videos.Add(MapPlaylistItemToEntity(item));
                        processedItems++;
                        if (maxResults.HasValue && processedItems >= maxResults)
                        {
                            return videos;
                        }
                        if (processedItems % 20 == 0)
                        {
                            YoutubeVideoEntity mostRecentEntity = videos.LastOrDefault();
                            _appLogger.Info($"Video {processedItems} - ID: {mostRecentEntity.YoutubeVideoId},"); 
                        }
                    }
                    nextPageToken = response.NextPageToken;

                    _appLogger.Info($"Retrieved {response.Items.Count} videos from playlist. Total videos so far: {videos.Count}");

                }
                while (!string.IsNullOrEmpty(nextPageToken));

                _appLogger.Info($"Successfully retrieved all videos from playlist. Total count: {videos.Count}");
                return videos;
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error retrieving videos for playlist {playlistId}: {ex.Message}");
                throw;
            }
        }


        public async Task<Dictionary<string, string>> GetChannelPlaylists(string channelId)
        {
            var playlists = new Dictionary<string, string>();
            var nextPageToken = "";

            try
            {
                do
                {
                    var playlistRequest = _youtubeService.Playlists.List("snippet");
                    playlistRequest.ChannelId = channelId;
                    playlistRequest.MaxResults = 50;
                    playlistRequest.PageToken = nextPageToken;

                    var playlistResponse = await playlistRequest.ExecuteAsync();

                    foreach (var playlist in playlistResponse.Items)
                    {
                        if (!playlists.ContainsKey(playlist.Id))
                        {
                            playlists.Add(playlist.Id, playlist.Snippet.Title);
                        }
                    }

                    nextPageToken = playlistResponse.NextPageToken;
                    _appLogger.Info($"Retrieved {playlistResponse.Items.Count} playlists from channel. " +
                                   $"Total playlists so far: {playlists.Count}");

                } while (!string.IsNullOrEmpty(nextPageToken));

                _appLogger.Info($"Successfully retrieved all playlists. Total count: {playlists.Count}");
                var samplePlaylists = playlists.Take(3)
                    .Select(p => $"\n\tPlaylist ID: {p.Key}, TeamName: {p.Value}");
                _appLogger.Info($"Sample playlists: {string.Join("", samplePlaylists)}");

                return playlists;
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error retrieving playlists for channel {channelId}: {ex.Message}");
                throw;
            }
        }

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
                VideoTitle = item.Snippet.Title,
                PublishedAt = item.Snippet.PublishedAt ?? DateTime.UtcNow,
                YoutubeResultHyperlink = $"https://www.youtube.com/watch?v={item.ContentDetails.VideoId}",
                ThumbnailUrl = item.Snippet.Thumbnails.Default__?.Url
            };

        }
    }
}