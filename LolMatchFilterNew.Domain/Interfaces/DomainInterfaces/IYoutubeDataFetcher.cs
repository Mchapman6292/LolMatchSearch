using LolMatchFilterNew.Domain.Entities.Import_YoutubeDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.IYoutubeDataFetcher
{
    public interface IYoutubeDataFetcher
    {
        Task<IEnumerable<Import_YoutubeDataEntity>> GetVideosFromChannel(string channelId, int? maxResults = null);
        Task<IEnumerable<Import_YoutubeDataEntity>> GetVideosFromPlaylist(string playlistId, int? maxResults = null);
        Task<Dictionary<string, string>> GetChannelPlaylists(string channelId);
        Task<string> GetChannelIdFromInput(string input);
    }
}
