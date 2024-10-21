using LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities;
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
        Task<IEnumerable<YoutubeVideoEntity>> MapYoutubeToEntity(IEnumerable<JObject> videoData);

        Task<IEnumerable<YoutubeVideoEntity>> MapYoutubeToEntityTesting(IEnumerable<JObject> videoData, int limit = 2);
    }
}
