using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IActivityService;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.IYoutubeDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IYoutubeMapper;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;


namespace LolMatchFilterNew.Domain.YoutubeService.YoutubeDataFetchers
{
    public class YoutubeDataFetcher : IYoutubeDataFetcher
    {
        private readonly string _apiKey;
        private readonly IAppLogger _appLogger;

        private readonly YouTubeService _youtubeService;
        private readonly IYoutubeMapper _youtubeMapper;

        private const int MaxResultsPerPage = 50;
        private const string RequiredPlaylistParts = "snippet,contentDetails";
        private const string RequiredChannelParts = "snippet";
        private const string KazaChannelId = "UC3Lh8yZe1MD-jCIXhBcVtqQ";


        public Dictionary<string, string> YoutubeplaylistNames = new Dictionary<string, string>();


        public YoutubeDataFetcher(IConfiguration configuration, IAppLogger appLogger, IYoutubeMapper youtubeMapper)
        {
            _apiKey = configuration["YouTubeApiKey"];
            _appLogger = appLogger;
            _youtubeMapper = youtubeMapper;
            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _apiKey,
                ApplicationName = "LolMatchFilter"
            });
        }



        /// <summary>
        /// Orchestrates the full process of fetching channel videos:
        /// 1. Gets all playlists using FetchChannelPlaylistIdNameAndId
        /// 2. For each playlist, calls FetchPlaylistItemAndMapToImport_YoutubeData
        /// 3. Combines all video entities into a single collection
        
        public async Task<IEnumerable<Import_YoutubeDataEntity>> FetchVideosFromChannel(int? maxResults = null)
        {
            var allVideos = new List<Import_YoutubeDataEntity>();
            var playlists = await FetchChannelPlaylistIdNameAndId();

            foreach (var playlist in playlists)
            {
                var videoEntities = await FetchPlaylistItemAndMapToImport_YoutubeData(playlist.Key, playlist.Value, maxResults);
                allVideos.AddRange(videoEntities);
            }

            _appLogger.Info($"Successfully retrieved videos from all playlists. Total videos: {allVideos.Count}");
            return allVideos;
        }






        /// <summary>
        /// Starting point - Gets all playlists from a channel.
        /// Returns a dictionary used by other methods to fetch video data:
        /// - Key: YouTube's PlaylistId required by FetchPlayListItem
        /// - Value: Playlist title needed for entity creation
       
        public async Task<Dictionary<string, string>> FetchChannelPlaylistIdNameAndId(string channelId = KazaChannelId)
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



        /// <summary>
        /// Intermediate step - Takes a playlist ID and retrieves its videos.
        /// Called by FetchPlaylistItemAndMapToImport_YoutubeData to get raw video data.
        /// Handles YouTube API pagination to get all videos in playlist.
        
        public async Task<IEnumerable<PlaylistItem>> FetchPlayListItem(string playlistId, int? maxResults = null)
        {
            var playlistItems = new List<PlaylistItem>();
            var nextPageToken = "";
            var processedItems = 0;

            do
            {
                var request = _youtubeService.PlaylistItems.List("snippet, contentDetails");
                request.PlaylistId = playlistId;
                request.MaxResults = 50;
                request.PageToken = nextPageToken;

                var response = await request.ExecuteAsync();

                playlistItems.AddRange(response.Items);
                processedItems += response.Items.Count();

                if (maxResults.HasValue && processedItems >= maxResults)
                {
                    return playlistItems.Take(maxResults.Value);
                }

                nextPageToken = response.NextPageToken;
                _appLogger.Info($"Retrieved {response.Items.Count} videos from playlist. Total videos so far: {playlistItems.Count}");
            }
            while (!string.IsNullOrEmpty(nextPageToken));

            return playlistItems;
        }



        /// <summary>
        /// Final step - Creates database entities from playlist videos:
        /// 1. Calls FetchPlayListItem to get video data
        /// 2. Maps each video to an entity using the playlist title
        /// Used directly by FetchVideosFromChannel or for single playlist processing

        public async Task<IEnumerable<Import_YoutubeDataEntity>> FetchPlaylistItemAndMapToImport_YoutubeData(string playlistId, string playlistTitle, int? maxResults = null)
        {
            var playlistItems = await FetchPlayListItem(playlistId, maxResults);
            var entities = new List<Import_YoutubeDataEntity>();

            foreach (var item in playlistItems)
            {
                entities.Add(await _youtubeMapper.MapToImport_YoutubeDataEntity(item, playlistTitle));
            }

            _appLogger.Info($"Successfully converted {entities.Count} playlist items to entities");
            return entities;
        }



        



        // 

     

        
    }
}