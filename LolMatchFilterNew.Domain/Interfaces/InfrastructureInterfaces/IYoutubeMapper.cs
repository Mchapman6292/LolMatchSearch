using LolMatchFilterNew.Domain.Entities.Import_YoutubeDataEntities;
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
        Task<IEnumerable<Import_YoutubeDataEntity>> MapYoutubeToEntity(IEnumerable<JObject> videoData);

        Task<IEnumerable<Import_YoutubeDataEntity>> MapYoutubeToEntityTesting(IEnumerable<JObject> videoData, int limit = 2);
    }
}
