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


namespace LolMatchFilterNew.Domain.YoutubeDataFetcher
{
    public class YoutubeDataFetcher
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
            _apiHelper.WritePlaylistsToDocxDocumentAsync(playlistNames);
        }
    }

    


}
