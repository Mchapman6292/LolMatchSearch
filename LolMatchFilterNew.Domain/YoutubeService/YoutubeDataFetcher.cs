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


        public async Task<List<YoutubeVideoEntity>> RetrieveAndMapAllPlaylistVideosToEntities(List<string> playlistIds)
        {
            var allVideos = new List<YoutubeVideoEntity>();
            var playlistNames = await GetPlaylistNames(playlistIds);

            foreach (var playlistId in playlistIds)
            {
                var videos = await GetVideosFromPlaylist(playlistId, playlistNames[playlistId]);
                allVideos.AddRange(videos);
            }
            var first = allVideos.FirstOrDefault();
            var last = allVideos.LastOrDefault();
            _appLogger.Info($"Retrieved {allVideos.Count} videos from {playlistIds.Count} playlists.");
            if (first != null)
            {
                _appLogger.Info("First video details:" +
                    $"\n\tYoutubeVideoId: {first.YoutubeVideoId}" +
                    $"\n\tTitle: {first.Title}" +
                    $"\n\tPlaylist Name: {first.PlaylistName ?? "N/A"}" +
                    $"\n\tPublished At: {first.PublishedAt:yyyy-MM-dd HH:mm:ss UTC}" +
                    $"\n\tYoutube URL: {first.YoutubeResultHyperlink}" +
                    $"\n\tThumbnail URL: {first.ThumbnailUrl ?? "N/A"}" +
                    $"\n\tLeaguepedia Game ID: {first.LeaguepediaGameIdAndTitle ?? "N/A"}" +
                    $"\n\tLeaguepedia Match: {(first.LeaguepediaMatch != null ? "Linked" : "Not Linked")}");
            }
            else
            {
                _appLogger.Warning("No first video found in the collection.");
            }

            if (last != null && (allVideos.Count > 1)) // Only log last if it's different from first
            {
                _appLogger.Info("Last video details:" +
                    $"\n\tYoutubeVideoId: {last.YoutubeVideoId}" +
                    $"\n\tTitle: {last.Title}" +
                    $"\n\tPlaylist Name: {last.PlaylistName ?? "N/A"}" +
                    $"\n\tPublished At: {last.PublishedAt:yyyy-MM-dd HH:mm:ss UTC}" +
                    $"\n\tYoutube URL: {last.YoutubeResultHyperlink}" +
                    $"\n\tThumbnail URL: {last.ThumbnailUrl ?? "N/A"}" +
                    $"\n\tLeaguepedia Game ID: {last.LeaguepediaGameIdAndTitle ?? "N/A"}" +
                    $"\n\tLeaguepedia Match: {(last.LeaguepediaMatch != null ? "Linked" : "Not Linked")}");
            }

            // Additional summary logging
            if (allVideos.Any())
            {
                var dateRange = $"{allVideos.Min(v => v.PublishedAt):yyyy-MM-dd} to {allVideos.Max(v => v.PublishedAt):yyyy-MM-dd}";
                var uniquePlaylists = allVideos.Select(v => v.PlaylistName).Distinct().Count();

                _appLogger.Info($"Video collection summary:" +
                    $"\n\tDate Range: {dateRange}" +
                    $"\n\tUnique Playlists: {uniquePlaylists}" +
                    $"\n\tTotal Videos: {allVideos.Count}");
            }

            return allVideos;
        }


        public async Task<Dictionary<string, string>> GetPlaylistNames(List<string> playlistIds)
        {
            var playlistRequest = _youtubeService.Playlists.List("snippet");
            playlistRequest.Id = string.Join(",", playlistIds);
            var playlistResponse = await playlistRequest.ExecuteAsync();

            return playlistResponse.Items.ToDictionary(
                playlist => playlist.Id,
                playlist => playlist.Snippet.Title
            );
        }

        public async Task<List<YoutubeVideoEntity>> GetVideosFromPlaylist(string playlistId, string playlistName)
        {
            var videos = new List<YoutubeVideoEntity>();
            var allItems = new List<PlaylistItem>();
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
                        PlaylistName = playlistName,
                        PublishedAt = _apiHelper.ConvertDateTimeOffSetToUTC(item.Snippet.PublishedAtDateTimeOffset),
                        YoutubeResultHyperlink = $"https://www.youtube.com/watch?v={item.ContentDetails.VideoId}",
                        ThumbnailUrl = item.Snippet.Thumbnails.Default__?.Url,
                        LeaguepediaGameIdAndTitle = null
                    });
                }

                nextPageToken = playlistItemsResponse.NextPageToken;
            } while (!string.IsNullOrEmpty(nextPageToken));

            return videos;
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
                .Select(p => $"\n\tPlaylist ID: {p.Key}, Name: {p.Value}");
            _appLogger.Info($"Sample playlists: {string.Join("", samplePlaylists)}");

            return playlists;
        }
        catch (Exception ex)
        {
            _appLogger.Error($"Error retrieving playlists for channel {channelId}: {ex.Message}");
            throw;
        }
    }




}


