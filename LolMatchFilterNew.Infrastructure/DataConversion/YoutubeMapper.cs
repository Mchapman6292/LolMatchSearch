using LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IYoutubeMapper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Infrastructure.DataConversion.YoutubeMappers
{
    public class YoutubeMapper: IYoutubeMapper
    {
        private readonly IAppLogger _appLogger;


        public YoutubeMapper
            (IAppLogger appLogger)
        {
            _appLogger = appLogger;
        }

        public async Task<IEnumerable<YoutubeVideoEntity>> MapYoutubeToEntity(IEnumerable<JObject> videoData)
        {

        }




    }
}
