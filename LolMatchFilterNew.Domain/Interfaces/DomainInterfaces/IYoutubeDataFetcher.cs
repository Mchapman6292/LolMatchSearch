using Google.Apis.YouTube.v3.Data;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.IYoutubeDataFetcher
{
    public interface IYoutubeDataFetcher
    {
        Task<IEnumerable<Import_YoutubeDataEntity>> FetchVideosFromChannel(int? maxResults = null);

        Task<Dictionary<string, string>> FetchChannelPlaylistIdNameAndId(string channelId);

        Task<IEnumerable<PlaylistItem>> FetchPlayListItem(string playlistId, int? maxResults = null);

        Task<IEnumerable<Import_YoutubeDataEntity>> FetchPlaylistItemAndMapToImport_YoutubeData(string playlistId, string playlistTitle, int? maxResults = null);

    }
}
