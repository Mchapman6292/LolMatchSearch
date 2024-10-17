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
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.ILeaguepediaControllers;
using LolMatchFilterNew.Application.QueryBuilders.LeaguepediaQueryService;
using LolMatchFilterNew.Domain.Apis.LeaguepediaDataFetcher;
using LolMatchFilterNew.Infrastructure.DataConversion.LeaguepediaApiMappers;
using Newtonsoft.Json.Linq;

namespace LolMatchFilterNew.Application.LeaguepediaControllers
{
    public class LeaguepediaController : ILeaguepediaController
    {
        private readonly IAppLogger _appLogger;
        private readonly ILeaguepediaQueryService _leaguepediaQueryService;
        private readonly ILeaguepediaDataFetcher _leaguepediaDataFetcher;
        private readonly ILeaguepediaApiMapper _leaguepediaApiMapper;
        private readonly ILeaguepediaMatchDetailRepository _leaguepediaMatchDetailRepository;


        public LeaguepediaController(IAppLogger appLogger, ILeaguepediaQueryService leaguepediaQueryService, ILeaguepediaDataFetcher leaguepediaDataFetcher, ILeaguepediaApiMapper leaguepediaApiMapper, ILeaguepediaMatchDetailRepository leaguepediaMatchDetailRepository)
        {
            _appLogger = appLogger;
            _leaguepediaQueryService = leaguepediaQueryService;
            _leaguepediaDataFetcher = leaguepediaDataFetcher;
            _leaguepediaApiMapper = leaguepediaApiMapper;
            _leaguepediaMatchDetailRepository = leaguepediaMatchDetailRepository;
        }


        public async Task FetchAndAddLeaguepediaData()
        {
            string query = _leaguepediaQueryService.BuildQueryStringForPlayersChampsInSeason("LoL EMEA Championship");

            IEnumerable<JObject> apiData = await _leaguepediaDataFetcher.FetchAndExtractMatches(query);

            IEnumerable<LeaguepediaMatchDetailEntity> leagueEntities = await _leaguepediaApiMapper.MapLeaguepediaDataToEntity(apiData);

            int addedEntries = await _leaguepediaMatchDetailRepository.BulkAddLeaguepediaMatchDetails(leagueEntities);

        }



    }
}
