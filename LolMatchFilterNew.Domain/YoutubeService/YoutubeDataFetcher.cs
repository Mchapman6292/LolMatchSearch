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


namespace LolMatchFilterNew.Domain.YoutubeDataFetcher
{
    public class YoutubeDataFetcher : IYoutubeDataFetcher
    {
        private readonly string _apiKey;
        private readonly IAppLogger _appLogger;
        private readonly IActivityService _activityService;
        private readonly YouTubeService _youtubeService;
        private readonly IApiHelper _apiHelper;


        public YoutubeDataFetcher(IConfiguration configuration, IAppLogger appLogger, IActivityService activityService, IApiHelper apiHelper )
        {
            _apiKey = configuration["YouTubeApiKey"];
            _appLogger = appLogger;
            _activityService = activityService;
            _apiHelper = apiHelper;
            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _apiKey,
                ApplicationName = "LolMatchFilter"
            });
        }


        public async Task GetAllPlaylistsFromChannel(string channelId)
        {
            var playlistNames = new List<string>();
            var nextPageToken = "";
            int totalResults = 0;
            do
            {
                var playlistRequest = _youtubeService.Playlists.List("snippet");
                playlistRequest.ChannelId = channelId;
                playlistRequest.MaxResults = 50;  
                playlistRequest.PageToken = nextPageToken;
                playlistRequest.Fields = "items(snippet(title)),nextPageToken";  

                var playlistResponse = await playlistRequest.ExecuteAsync();

                foreach (var playlist in playlistResponse.Items)
                {
                    playlistNames.Add(playlist.Snippet.Title);
                    totalResults++;
                }

                nextPageToken = playlistResponse.NextPageToken;

            } while (!string.IsNullOrEmpty(nextPageToken));

            _appLogger.Info($"Total playlists: {totalResults}");
            await _apiHelper.WritePlaylistsToDocxDocumentAsync(playlistNames);
        }

        public async Task<List<YoutubeVideoEntity>> GetLECHighlightVideos()
        {
            var playlistIds = new List<string>
        {
            "PLJwuLHutaYuLMHzkyblz2q0HlDd7otgJA",
            "PLJwuLHutaYuIeTnqWqz4V9Jh-FCwGTpCf",
            "PLJwuLHutaYuKP5Pmd8Ry233MM0j6m1"
        };

            var allVideos = new List<YoutubeVideoEntity>();

            foreach (var playlistId in playlistIds)
            {
                var playlistVideos = await GetPlaylistVideos(playlistId);
                allVideos.AddRange(playlistVideos);
            }

            _appLogger.Info($"Retrieved {allVideos.Count} LEC highlight videos from the specified playlists.");
            return allVideos;
        }

        private async Task<List<YoutubeVideoEntity>> GetPlaylistVideos(string playlistId)
        {
            var videos = new List<YoutubeVideoEntity>();
            var nextPageToken = "";

            do
            {
                var playlistItemsRequest = _youtubeService.PlaylistItems.List("snippet,contentDetails");
                playlistItemsRequest.PlaylistId = playlistId;
                playlistItemsRequest.MaxResults = 50;
                playlistItemsRequest.PageToken = nextPageToken;

                var playlistItemsResponse = await playlistItemsRequest.ExecuteAsync();

                foreach (var item in playlistItemsResponse.Items)
                {
                    videos.Add(new YoutubeVideoEntity
                    {
                        YoutubeVideoId = item.ContentDetails.VideoId,
                        Title = item.Snippet.Title,
                        PlaylistName = await GetPlaylistName(playlistId),
                        PublishedAt = item.Snippet.PublishedAt ?? DateTime.MinValue,
                        YoutubeResultHyperlink = $"https://www.youtube.com/watch?v={item.ContentDetails.VideoId}",
                        ThumbnailUrl = item.Snippet.Thumbnails.Default__?.Url,
   
                    });
                }

                nextPageToken = playlistItemsResponse.NextPageToken;
            } while (!string.IsNullOrEmpty(nextPageToken));

            return videos;
        }

        private async Task<string> GetPlaylistName(string playlistId)
        {
            // Implement caching mechanism to avoid repeated API calls for the same playlist
            if (_playlistNameCache.TryGetValue(playlistId, out string cachedName))
            {
                return cachedName;
            }

            var playlistRequest = _youtubeService.Playlists.List("snippet");
            playlistRequest.Id = playlistId;
            var playlistResponse = await playlistRequest.ExecuteAsync();

            if (playlistResponse.Items.Any())
            {
                string playlistName = playlistResponse.Items[0].Snippet.Title;
                _playlistNameCache[playlistId] = playlistName;
                return playlistName;
            }

            return "Unknown Playlist";
        }






    }

}
