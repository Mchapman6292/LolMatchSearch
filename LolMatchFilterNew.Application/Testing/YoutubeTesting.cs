using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.IYoutubeDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Application.Testing.YoutubeTesting
{
    public class YoutubeTesting
    {
        private readonly IYoutubeDataFetcher _youtubeDataFetcher;
        private readonly IImport_YoutubeDataRepository _youtubeVideoRepository;
        private readonly IAppLogger _appLogger;

        public YoutubeTesting(IAppLogger appLogger,IYoutubeDataFetcher youtubeDataFetcher, IImport_YoutubeDataRepository youtubeVideoRepository)
        {
            _appLogger = appLogger;
            _youtubeDataFetcher = youtubeDataFetcher;
            _youtubeVideoRepository = youtubeVideoRepository;
        }


        public Dictionary<string, int> GetCountsOfGames(List<string> games)
        {
            throw new NotImplementedException();


        }
    }
}
