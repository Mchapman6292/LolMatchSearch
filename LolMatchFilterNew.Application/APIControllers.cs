using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryService;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaApiMappers;
using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaMatchDetailRepository;
using LolMatchFilterNew.Domain.Interfaces.ILeaguepediaDataFetcher;
using Microsoft.Extensions.DependencyInjection;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IAPIControllers;
using LolMatchFilterNew.Application.QueryBuilders.LeaguepediaQueryService;
using LolMatchFilterNew.Domain.Apis.LeaguepediaDataFetcher;
using LolMatchFilterNew.Infrastructure.DataConversion.LeaguepediaApiMappers;
using Newtonsoft.Json.Linq;
using LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.IYoutubeDataFetcher;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IYoutubeVideoRepository;
using LolMatchFilterNew.Domain.Entities.YoutubeVideoEntities; 

namespace LolMatchFilterNew.Application.LeaguepediaControllers
{
    public class APIControllers : IAPIControllers
    {
        private readonly IAppLogger _appLogger;
        private readonly ILeaguepediaQueryService _leaguepediaQueryService;
        private readonly ILeaguepediaDataFetcher _leaguepediaDataFetcher;
        private readonly ILeaguepediaApiMapper _leaguepediaApiMapper;
        private readonly ILeaguepediaMatchDetailRepository _leaguepediaMatchDetailRepository;
        private readonly IYoutubeDataFetcher _youtubeDataFetcher;
        private readonly IYoutubeVideoRepository _youtubeVideoRepository;


        public APIControllers(IAppLogger appLogger, ILeaguepediaQueryService leaguepediaQueryService, ILeaguepediaDataFetcher leaguepediaDataFetcher, ILeaguepediaApiMapper leaguepediaApiMapper, ILeaguepediaMatchDetailRepository leaguepediaMatchDetailRepository, IYoutubeDataFetcher youtubeDataFetcher, IYoutubeVideoRepository youtubeVideoRepository)
        {
            _appLogger = appLogger;
            _leaguepediaQueryService = leaguepediaQueryService;
            _leaguepediaDataFetcher = leaguepediaDataFetcher;
            _leaguepediaApiMapper = leaguepediaApiMapper;
            _leaguepediaMatchDetailRepository = leaguepediaMatchDetailRepository;
            _youtubeDataFetcher = youtubeDataFetcher;
            _youtubeVideoRepository = youtubeVideoRepository;
        }


        public async Task FetchAndAddLeaguepediaData(string leagueName)
        {
            int limit = 5;

       
            IEnumerable<JObject> apiData = await _leaguepediaDataFetcher.FetchAndExtractMatches(leagueName);

            IEnumerable<LeaguepediaMatchDetailEntity> leagueEntities = await _leaguepediaApiMapper.MapLeaguepediaDataToEntity(apiData);

            int addedEntries = await _leaguepediaMatchDetailRepository.BulkAddLeaguepediaMatchDetails(leagueEntities);

        }

        public async Task FetchAndAddYoutubeVideo(List<string> playlistIds)
        {
            IList<YoutubeVideoEntity> retrievedEntities = await _youtubeDataFetcher.RetrieveAndMapAllPlaylistVideosToEntities(playlistIds);

            await _youtubeVideoRepository.BulkaddYoutubeDetails(retrievedEntities);
        }






    }
}
