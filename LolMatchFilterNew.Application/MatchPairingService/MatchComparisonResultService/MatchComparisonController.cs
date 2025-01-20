using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchServiceControllers;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchComparisonResultBuilders;
using Domain.DTOs.TeamnameDTOs;
using Domain.Interfaces.ApplicationInterfaces.ITeamNameDTOBuilders;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;
using Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers;
using Application.MatchPairingService.YoutubeDataService.YoutubeDataWithTeamsDTOBuilders;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeDataWithTeamsDTOBuilders;
using Domain.DTOs.YoutubeDataWithTeamsDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using Domain.Interfaces.ApplicationInterfaces.IMatchDTOServices.IImport_TeamNameServices;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamNameServices;
using Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers;
using Application.MatchPairingService.ScoreboardGameService.MatchDTOServices.TeamNameServices.Import_TeamNameServices;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamNameFinders;
using Application.MatchPairingService.YoutubeDataService.YoutubeTitleTeamNameMatchResults;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamMatchCountFactories;
using Application.MatchPairingService.YoutubeDataService.YoutubeTeamNameServices;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_Teamnames;
using Domain.DTOs.Western_MatchDTOs;
using Domain.Interfaces.InfrastructureInterfaces.IImport_TeamnameRepositories;
using Domain.DTOs.PlayListDateRangeDTOs;
using Domain.Interfaces.ApplicationInterfaces.IDTOBuilders.IPlayListDateRangeDTOFactories;

namespace Application.MatchPairingService.MatchComparisonResultService.MatchComparisonControllers
{
    public class MatchComparisonController : IMatchComparisonController
    {
        private readonly IAppLogger _appLogger;
        private readonly IObjectLogger _objectLogger;
        private readonly IMatchComparisonResultBuilder _matchBuilder;
        private readonly ITeamNameDTOBuilder _teamNameDTOBuilder;
        private readonly IStoredSqlFunctionCaller _storedSqlFunctionCaller;
        private readonly IImport_YoutubeDataRepository _import_YoutubeDataRepository;
        private readonly IImport_TeamnameRepository _import_TeamNameRepository;
        private readonly IYoutubeDataWithTeamsDTOBuilder _processed_YoutubeDataDTOBuilder;
        private readonly IPlayListDateRangeDTOFactory _playListDateRangeDTOFactory;


        private readonly IYoutubeTeamNameService _youtubeTeamNameService;
        private readonly IImport_TeamNameService _importTeamNameService;


        private readonly IYoutubeTitleTeamNameFinder _youtubeTitleTeamNameFinder;
        private readonly IYoutubeTitleTeamMatchCountFactory _youtubeTitleTeamMatchCountFactory;




        public MatchComparisonController(

            IAppLogger appLogger,
            IObjectLogger objectLogger,
            IMatchComparisonResultBuilder matchComparisonResultBuilder,
            ITeamNameDTOBuilder teamnameDTOBuilder,
            IImport_YoutubeDataRepository import_YoutubeDataRepository,
            IStoredSqlFunctionCaller storedSqlFunctionCaller,
            IYoutubeDataWithTeamsDTOBuilder proccessed_YoutubeDataDTOBuilder,
            IPlayListDateRangeDTOFactory playListDateRangeDTOFactory,

            IImport_TeamNameService import_TeamnNameService,
            IYoutubeTeamNameService youtubeTeamNameService,
            IImport_TeamnameRepository import_TeamnameRepository,

            IYoutubeTitleTeamNameFinder youtubeTitleTeamNameFinder,
            IYoutubeTitleTeamMatchCountFactory youtubeTitleTeamMatchCountFactory
            

        )
        {
            _appLogger = appLogger;
            _objectLogger = objectLogger;
            _matchBuilder = matchComparisonResultBuilder;

            _teamNameDTOBuilder = teamnameDTOBuilder;
            _import_YoutubeDataRepository = import_YoutubeDataRepository;
            _import_TeamNameRepository = import_TeamnameRepository;
            _storedSqlFunctionCaller = storedSqlFunctionCaller;
            _processed_YoutubeDataDTOBuilder = proccessed_YoutubeDataDTOBuilder;
            _playListDateRangeDTOFactory = playListDateRangeDTOFactory;

            _youtubeTeamNameService = youtubeTeamNameService;
            _youtubeTitleTeamNameFinder = youtubeTitleTeamNameFinder;
            _youtubeTitleTeamMatchCountFactory = youtubeTitleTeamMatchCountFactory;
            _importTeamNameService = import_TeamnNameService;


        }


        // 


        public async Task TESTFindTeamNameMatchesInYoutubeTitleAsync()
        {
            List<Import_TeamnameEntity> teamnNameEntities = await _storedSqlFunctionCaller.GetAllWesternTeamsAsync();
            List<Import_YoutubeDataEntity> importYoutubeEntities = await _storedSqlFunctionCaller.GetYoutubeDataEntitiesForWesternTeamsAsync();

            var startDate = new DateTime(2015, 1, 1);

            var filteredYoutubeEntities = importYoutubeEntities
                .Where(x => x.PublishedAt_utc.HasValue && x.PublishedAt_utc.Value > startDate)
                .ToList();

            List<TeamNameDTO> teamNameDtos = _importTeamNameService.BuildTeamNameDTOFromImport_TeamNameEntites(teamnNameEntities);

            _appLogger.Info($"TeamNameDTO list build with count: {teamNameDtos.Count}.");

            var youtubeEntitiesSample = filteredYoutubeEntities
                .OrderBy(x => Guid.NewGuid())
                .Take(100)
                .ToList();



            _importTeamNameService.PopulateImport_TeamNameAllNames(teamNameDtos);
            _youtubeTeamNameService.PopulateYoutubeTitleTeamMatchCountList(youtubeEntitiesSample);

            List<YoutubeTitleTeamNameMatchResult> teamNameOccurences = _youtubeTeamNameService.ReturnYoutubeTitleTeamMatchCounts();

            foreach(var youtubeOccurenceDto in teamNameOccurences)
            {
                _youtubeTitleTeamNameFinder.ProcessYoutubeTitle(youtubeOccurenceDto);
            }


            _objectLogger.LogTopYoutubeTeamNameOccurenceMatches(teamNameOccurences);

        }


        public async Task TestGetPlaylistDateRanges()
        {
            List<Import_YoutubeDataEntity> youtubeEntities = await _import_YoutubeDataRepository.GetEuNaVideosByPlaylistAsync();

            List<PlayListDateRangeDTO> playListRangeDtos = _playListDateRangeDTOFactory.CreateListOfPlaylistDateRangeDTOs(youtubeEntities);

            _objectLogger.LogPlaylistDateRanges(playListRangeDtos);




        }













    }
}













    


   




















