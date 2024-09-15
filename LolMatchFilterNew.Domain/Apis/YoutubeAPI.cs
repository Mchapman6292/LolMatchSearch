using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IYoutubeApi;
using LolMatchFilterNew.Domain.Interfaces.ILeaguepediaApis;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using Microsoft.Extensions.Configuration;
using Google.Apis.Services;
using Google.Apis.YouTube;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.YouTube.v3;

using LolMatchFilterNew.Domain.Entities.YoutubeVideoData;
using Activity = System.Diagnostics.Activity;
using LolMatchFilterNew.Domain.Entities;
using System.Drawing;
using System.Text.RegularExpressions;
using LolMatchFilterNew.Domain.Interfaces.IYoutubeTitleMatcher;
using LolMatchFilterNew.Domain.Interfaces.IActivityService;
using System.Linq.Expressions;


namespace LolMatchFilterNew.Domain.Apis.YoutubeApi
{
    public class YoutubeApi : IYoutubeApi
    {
        private readonly string _apiKey;
        private static readonly string SaveDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LolMatchReports");
        private readonly YouTubeService _youtubeService;
        private readonly IAppLogger _appLogger;
        private readonly IApiHelper _apiHelper;
        private readonly IYoutubeTitleMatcher _youtubeTitleMatcher;
        private readonly IActivityService _activityService;
        private const string BaseUrl = "https://www.googleapis.com/youtube/v3";

        public YoutubeApi(IConfiguration configuration, IAppLogger appLgger, IApiHelper apiHelper, IYoutubeTitleMatcher youtubeTitleMatcher, IActivityService activityService)
        {
            _apiKey = configuration["YouTubeApiKey"];
            _appLogger = appLgger;
            _apiHelper = apiHelper;
            _youtubeTitleMatcher = youtubeTitleMatcher;
            _activityService = activityService;
            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _apiKey,
                ApplicationName = "LolMatchFilter"
            });




        }


        public async Task<List<PlaylistItem>> GetYoutubeVideosFromPlayList(Activity activity, string playlistId)
        {
            _appLogger.Info($"Starting {nameof(GetYoutubeVideosFromPlayList)} TraceId: {activity.TraceId}, ParentId: {activity.ParentId}.");

            List<PlaylistItem> playListResults = new List<PlaylistItem>();
            var nextPageToken = "";

            do
            {
                var playlistItemsListRequest = _youtubeService.PlaylistItems.List("snippet");
                playlistItemsListRequest.PlaylistId = playlistId;
                playlistItemsListRequest.MaxResults = 50;
                playlistItemsListRequest.PageToken = nextPageToken;

                try
                {
                    var playlistItemsListResponse = await playlistItemsListRequest.ExecuteAsync();

                    playListResults.AddRange(playlistItemsListResponse.Items);

                    nextPageToken = playlistItemsListResponse.NextPageToken;

                }
                catch (Exception ex)
                {
                    _appLogger.Error($"Error during {nameof(GetYoutubeVideosFromPlayList)}.", ex);
                    throw;
                }
            }
            while (!string.IsNullOrEmpty(nextPageToken));
            {
                _appLogger.Info($"Retrieved {playListResults.Count} videos from playlist {playlistId}");
                return playListResults;
            }
        }




        public async Task GetAndDocumentVideoDataAsync(Activity activity, string videoTitle, string gameId, List<string> teamNames)
        {
            _appLogger.Info($"Starting {nameof(GetAndDocumentVideoDataAsync)} TraceId: {activity.TraceId}, ParentId: {activity.ParentId}.");

            List<string> videoDetails = new List<string>();
            videoDetails.Add($"Search Query: '{videoTitle}'");
            videoDetails.Add($"Game ID: {gameId}");
            videoDetails.Add($"Expected Teams: {string.Join(", ", teamNames)}");
            videoDetails.Add("");

            try
            {
                var searchListRequest = _youtubeService.Search.List("snippet");
                searchListRequest.Q = videoTitle;
                searchListRequest.Type = "video";
                searchListRequest.MaxResults = 3;

                _appLogger.Info($"Executing YouTube search for title: '{videoTitle}'. TraceId: {activity.TraceId}, ParentId: {activity.ParentId}.");
                var searchResponse = await searchListRequest.ExecuteAsync();

                if (searchResponse.Items.Count == 0)
                {
                    _appLogger.Warning($"No videos found with title: '{videoTitle}'. TraceId: {activity.TraceId}, ParentId: {activity.ParentId}.");
                    videoDetails.Add($"No videos found with title: '{videoTitle}'");
                }
                else
                {
                    _appLogger.Info($"Found {searchResponse.Items.Count} video(s). Checking for match. TraceId: {activity.TraceId}, ParentId: {activity.ParentId}.");
                    videoDetails.Add($"Found {searchResponse.Items.Count} video(s). Checking for match.");

                    bool matchFound = false;
                    foreach (var searchResult in searchResponse.Items)
                    {
                        var videoId = searchResult.Id.VideoId;
                        _appLogger.Info($"Fetching details for video ID: {videoId}. TraceId: {activity.TraceId}, ParentId: {activity.ParentId}.");

                        var videoRequest = _youtubeService.Videos.List("snippet,contentDetails");
                        videoRequest.Id = videoId;

                        var videoResponse = await videoRequest.ExecuteAsync();

                        if (videoResponse.Items.Count > 0)
                        {
                            var videoItem = videoResponse.Items[0];
                            var retrievedTitle = videoItem.Snippet.Title;

                            _appLogger.Info($"Validating video: '{retrievedTitle}'. TraceId: {activity.TraceId}, ParentId: {activity.ParentId}.");
                            videoDetails.Add($"Validating video: '{retrievedTitle}'");

                            if (await _youtubeTitleMatcher.CheckYoutubeTitleFormatForAbbreviatedTeams(activity, retrievedTitle))
                            {
                                var extractedTeams = await _youtubeTitleMatcher.ExtractTeamNames(activity, retrievedTitle);
                                if (extractedTeams.Count == 2 && teamNames.All(team => extractedTeams.Contains(team, StringComparer.OrdinalIgnoreCase)))
                                {
                                    _appLogger.Info($"Match found for GameId: {gameId}. Video: '{retrievedTitle}'. TraceId: {activity.TraceId}, ParentId: {activity.ParentId}.");

                                    videoDetails.Add("Match found:");
                                    videoDetails.Add($"Video ID: {videoId}");
                                    videoDetails.Add($"Title: {retrievedTitle}");
                                    videoDetails.Add($"Description: {videoItem.Snippet.Description}");
                                    videoDetails.Add($"Published At: {videoItem.Snippet.PublishedAt}");
                                    videoDetails.Add($"Thumbnail URL: {videoItem.Snippet.Thumbnails.Default__.Url}");
                                    videoDetails.Add($"Duration: {videoItem.ContentDetails.Duration}");

                                    matchFound = true;
                                    break;
                                }
                            }

                            if (!matchFound)
                            {
                                _appLogger.Info($"Video did not match criteria. Title: '{retrievedTitle}'. TraceId: {activity.TraceId}, ParentId: {activity.ParentId}.");
                                videoDetails.Add($"Video did not match criteria. Title: '{retrievedTitle}'");
                            }
                        }
                    }

                    if (!matchFound)
                    {
                        _appLogger.Warning($"No matching video found for GameId: {gameId}. TraceId: {activity.TraceId}, ParentId: {activity.ParentId}.");
                        videoDetails.Add($"No matching video found for GameId: {gameId}");
                    }
                }
            }
            catch (Google.GoogleApiException gEx)
            {
                _appLogger.Error($"Google API error in {nameof(GetAndDocumentVideoDataAsync)}. TraceId: {activity.TraceId}, ParentId: {activity.ParentId}", gEx);
                videoDetails.Add($"Google API error: {gEx.Message}");
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Unexpected error in {nameof(GetAndDocumentVideoDataAsync)}. TraceId: {activity.TraceId}, ParentId: {activity.ParentId}", ex);
                videoDetails.Add($"Unexpected error: {ex.Message}");
            }
            finally
            {
                string filePath = await _apiHelper.WriteToDocxDocumentAsync(activity, "YoutubeVideoData", videoDetails);
                _appLogger.Info($"YouTube video data report saved to: {filePath}. TraceId: {activity.TraceId}, ParentId: {activity.ParentId}.");
                Console.WriteLine($"YouTube video data report saved to: {filePath}");
            }
        }






        public async Task<List<PlaylistItem>> GetYoutubeVideoPlaylistAsync(string videoId)
        {
            Activity activity = null;
            try
            {
                activity = await _activityService.StartActivityAsync(nameof(GetYoutubeVideoPlaylistAsync));

                var playlistItemsListRequest = _youtubeService.PlaylistItems.List("snippet");
                playlistItemsListRequest.VideoId = videoId;
                playlistItemsListRequest.MaxResults = 50;

                var playlistItemsListResponse = await playlistItemsListRequest.ExecuteAsync();

                if (playlistItemsListResponse.Items.Count > 0)
                {
                    _appLogger.Info($"The video with ID {videoId} is in {playlistItemsListResponse.Items.Count} playlists.");
                    return playlistItemsListResponse.Items.ToList();
                }
                else
                {
                    _appLogger.Info($"The video with ID {videoId} is not in any playlists.");
                    return new List<PlaylistItem>();
                }
            }
            catch (Exception ex)
            {
                _appLogger.Error($"An error occurred while fetching playlists for video {videoId}.", ex);
                throw;
            }
            finally
            {
                if (activity != null)
                {
                    await _activityService.StopActivityAsync(activity);
                }
            }
        }
    }
}
