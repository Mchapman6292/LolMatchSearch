using LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.IYoutubeDataFetcher
{
    public interface IYoutubeDataFetcher
    {
        Task<List<YoutubeVideoEntity>> RetrieveAndMapAllPlaylistVideosToEntities(List<string> playlistIds);

        Task<Dictionary<string, string>> GetPlaylistNames(List<string> playlistIds);

        Task<List<YoutubeVideoEntity>> GetVideosFromPlaylist(string playlistId, string playlistName);

        Task<Dictionary<string, string>> GetChannelPlaylists(string channelId);
    }
}
