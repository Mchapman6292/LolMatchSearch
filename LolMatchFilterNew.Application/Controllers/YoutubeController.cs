using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IYoutubeVideoRepository;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.IYoutubeDataFetcher;
using LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Application.Controllers.YoutubeControllers
{
    public class YoutubeController
    {
        private readonly IAppLogger _appLogger;
        private readonly IYoutubeDataFetcher _youtubeDataFetcher;
        private readonly IYoutubeVideoRepository _youtubeVideoRepository;


        public YoutubeController(IAppLogger appLogger, IYoutubeDataFetcher youtubeDataFetcher, IYoutubeVideoRepository youtubeVideoRepository)
        {
            _appLogger = appLogger;
            _youtubeDataFetcher = youtubeDataFetcher;
            _youtubeVideoRepository = youtubeVideoRepository;
        }

        public async Task FetchAndAddYoutubeVideo(List<string> playlistIds)
        {
            IList<YoutubeVideoEntity> retrievedEntities = await _youtubeDataFetcher.RetrieveAndMapAllPlaylistVideosToEntities(playlistIds);

            await _youtubeVideoRepository.BulkaddYoutubeDetails(retrievedEntities);
        }
    }
}
