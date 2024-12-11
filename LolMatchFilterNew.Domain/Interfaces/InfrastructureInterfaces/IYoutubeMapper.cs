using Google.Apis.YouTube.v3.Data;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IYoutubeMapper
{
    public interface IYoutubeMapper
    {
        Task<Import_YoutubeDataEntity> MapToImport_YoutubeDataEntity(PlaylistItem item, string playlistTitle);

        Task<IEnumerable<Import_YoutubeDataEntity>> MapYoutubeToEntityTesting(IEnumerable<JObject> videoData, int limit = 2);
    }
}
