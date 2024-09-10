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
using Microsoft.Extensions.Configuration;
using Google.Apis.Services;
using Google.Apis.YouTube;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.YouTube.v3;

using LolMatchFilterNew.Domain.Entities.YoutubeVideoData;
using Activity = System.Diagnostics.Activity;


namespace LolMatchFilterNew.Domain.Apis
{
    public class YoutubeApi : IYoutubeApi
    {
        private readonly string _apiKey;
        private static readonly string SaveDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LolMatchReports");
        private readonly YouTubeService _youtubeService;
        private readonly IAppLogger _appLogger;
        private readonly ILeaguepediaApi _leaguepediaApi;
        private const string BaseUrl = "https://www.googleapis.com/youtube/v3";

        public YoutubeApi(IConfiguration configuration, IAppLogger appLgger, ILeaguepediaApi leaguepediaApi)
        {
            var apiKey = configuration["YouTubeApiKey"];
            _appLogger = appLgger;
            _leaguepediaApi = leaguepediaApi;
            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _apiKey,
                ApplicationName = "LolMatchFilter"
            });




        }

        public async Task GetAndDocumentVideoDataAsync(string videoTitle, string gameId, Activity activity)
        {
            _appLogger.Info($"Starting {nameof(GetAndDocumentVideoDataAsync)} TraceId: {activity.TraceId}, ParentId: {activity.ParentId}.");

            List<string> videoDetails = new List<string>();
            videoDetails.Add($"Search Query: '{videoTitle}'");
            videoDetails.Add($"Game ID: {gameId}");
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

                            if (ValidateVideoMatch(gameId, retrievedTitle))
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
                            else
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
                string filePath = await _leaguepediaApi.WriteToDocxDocumentAsync("YoutubeVideoData", videoDetails);
                _appLogger.Info($"YouTube video data report saved to: {filePath}. TraceId: {activity.TraceId}, ParentId: {activity.ParentId}.");
                Console.WriteLine($"YouTube video data report saved to: {filePath}");
            }
        }


        private bool ValidateVideoMatch(string gameId, string videoTitle)
        {
            var gameIdParts = gameId.Split('/');
            var gameParts = gameIdParts[2].Split('_');

            var league = gameIdParts[0];
            var season = gameIdParts[1];
            var stage = gameParts[0];
            var round = gameParts[1];
            var gameNumber = gameParts[3];

            var titleLower = videoTitle.ToLower();

            bool leagueMatch = titleLower.Contains(league.ToLower());
            bool seasonMatch = titleLower.Contains(season.ToLower());
            bool stageMatch = titleLower.Contains(stage.ToLower());
            bool roundMatch = titleLower.Contains(round.ToLower());
            bool gameNumberMatch = titleLower.Contains($"game {gameNumber}");


 
            return leagueMatch && seasonMatch && stageMatch && roundMatch && gameNumberMatch;
        }

        public async Task GetPlaylistVideosAsync(System.Diagnostics.Activity activity, string playlistId)
        {
        }
    }
}