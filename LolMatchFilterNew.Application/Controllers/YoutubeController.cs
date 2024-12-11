using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.IYoutubeDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IYoutubeController;
using Google.Apis.YouTube.v3.Data;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using System.Threading.Channels;

namespace LolMatchFilterNew.Application.Controllers.YoutubeControllers
{
    public class YoutubeController : IYoutubeController
    {
        private readonly IAppLogger _appLogger;
        private readonly IYoutubeDataFetcher _youtubeDataFetcher;
        private readonly IImport_YoutubeDataRepository _youtubeVideoRepository;
        private readonly IApiHelper _apiHelper;
        private readonly ILeaguepediaQueryService _leaguepediaQueryService;
        private const string KazaChannelId = "UC3Lh8yZe1MD-jCIXhBcVtqQ";


        public YoutubeController(IAppLogger appLogger, IYoutubeDataFetcher youtubeDataFetcher, IImport_YoutubeDataRepository youtubeVideoRepository, IApiHelper apiHelper, ILeaguepediaQueryService leaguepediaQueryService)
        {
            _appLogger = appLogger;
            _youtubeDataFetcher = youtubeDataFetcher;
            _youtubeVideoRepository = youtubeVideoRepository;
            _apiHelper = apiHelper;
            _leaguepediaQueryService = leaguepediaQueryService;
        }


  
 


        // Used to add all entries to Import_youtubeData - 11/12/2024

        public async Task ControllerAddAllImport_YoutubeData()
        {
            var videos = await _youtubeDataFetcher.FetchVideosFromChannel();

            await _youtubeVideoRepository.BulkaddYoutubeDetails(videos);
        }





        // Testing method used to add all videos from one playlist to Import_YoutubeData
        // Youtube playlist - https://www.youtube.com/watch?v=4AItVpg8TlM&list=PLJwuLHutaYuKj_klwtSG1bNPVjZ73QM1s
        // LEC Summer 2024 ALL GAMES Highlights
        public async Task TESTControllerAddOneImport_YoutubeData()
        {
            string playListId = "PLJwuLHutaYuKj_klwtSG1bNPVjZ73QM1s";

            string playlistTitle = "LEC Summer 2024 ALL GAMES Highlights";

            var videos = await _youtubeDataFetcher.FetchPlaylistItemAndMapToImport_YoutubeData(playListId, playlistTitle);

            await _youtubeVideoRepository.BulkaddYoutubeDetails(videos);
        }

    }
}
