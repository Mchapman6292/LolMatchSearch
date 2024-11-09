﻿using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IYoutubeVideoRepository;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.IYoutubeDataFetcher;
using LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IYoutubeController;

namespace LolMatchFilterNew.Application.Controllers.YoutubeControllers
{
    public class YoutubeController : IYoutubeController
    {
        private readonly IAppLogger _appLogger;
        private readonly IYoutubeDataFetcher _youtubeDataFetcher;
        private readonly IYoutubeVideoRepository _youtubeVideoRepository;
        private readonly IApiHelper _apiHelper;
        private readonly ILeaguepediaQueryService _leaguepediaQueryService;


        public YoutubeController(IAppLogger appLogger, IYoutubeDataFetcher youtubeDataFetcher, IYoutubeVideoRepository youtubeVideoRepository, IApiHelper apiHelper, ILeaguepediaQueryService leaguepediaQueryService)
        {
            _appLogger = appLogger;
            _youtubeDataFetcher = youtubeDataFetcher;
            _youtubeVideoRepository = youtubeVideoRepository;
            _apiHelper = apiHelper;
            _leaguepediaQueryService = leaguepediaQueryService;
        }

        public async Task FetchAndAddYoutubeVideo(List<string> playlistIds)
        {
            IList<YoutubeVideoEntity> retrievedEntities = await _youtubeDataFetcher.RetrieveAndMapAllPlaylistVideosToEntities(playlistIds);

            await _youtubeVideoRepository.BulkaddYoutubeDetails(retrievedEntities);
        }

        public async Task FetchAndAddYoutubePlaylistsForChannel()
        {
            string channelId = "UC3Lh8yZe1MD-jCIXhBcVtqQ";

            Dictionary<string, string> results =  await _youtubeDataFetcher.GetChannelPlaylists(channelId);

            await _apiHelper.WriteListDictToWordDocAsync(results);
        }

        public async Task FetchAndAddAllTeamNamesForRegion()
        {
            
        }
    }
}