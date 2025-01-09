using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchServiceControllers;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchComparisonResultBuilders;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamExtractors;
using Domain.DTOs.TeamnameDTOs;
using Domain.Interfaces.ApplicationInterfaces.ITeamNameDTOBuilders;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;
using Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers;
using Domain.Interfaces.ApplicationInterfaces.ITeamNameValidators;
using Application.MatchPairingService.YoutubeDataService.Processed_YoutubeDataDTOBuilder;
using Domain.Interfaces.ApplicationInterfaces.IProcessed_YoutubeDataDTOBuilders;
using Domain.DTOs.Processed_YoutubeDataDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using Domain.Interfaces.ApplicationInterfaces.IMatchDTOServices.IScoreboardGamesTeamNameServices;

namespace Application.MatchPairingService.MatchComparisonResultService.MatchComparisonControllers
{
    public class MatchComparisonController : IMatchComparisonController
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchComparisonResultBuilder _matchBuilder;
        private readonly IYoutubeTeamExtractor _youtubeTeamExtractor;
        private readonly ITeamNameDTOBuilder _teamnameDTOBuilder;
        private readonly IStoredSqlFunctionCaller _storedSqlFunctionCaller;
        private readonly IImport_YoutubeDataRepository _import_YoutubeDataRepository;
        private readonly IProcessed_YoutubeDataDTOBuilder _processed_YoutubeDataDTOBuilder;
        private readonly IScoreboardGamesTeamNameService _teamNameService;
        private readonly List<TeamnameDTO> _KnownTeamNamesAndAbbreviations;





        public MatchComparisonController(

            IAppLogger appLogger,
            IMatchComparisonResultBuilder matchComparisonResultBuilder,
            ITeamNameDTOBuilder teamnameDTOBuilder,
            IScoreboardGamesTeamNameService teamNameService,
            IImport_YoutubeDataRepository import_YoutubeDataRepository,
            IStoredSqlFunctionCaller storedSqlFunctionCaller,
            IProcessed_YoutubeDataDTOBuilder proccessed_YoutubeDataDTOBuilder,
            IYoutubeTeamExtractor youtubeTeamExtractor
            
        )
        {
            _appLogger = appLogger;
            _matchBuilder = matchComparisonResultBuilder;

            _teamnameDTOBuilder = teamnameDTOBuilder;
            _teamNameService = teamNameService;
            _KnownTeamNamesAndAbbreviations = _teamNameService.GetTeamNamesAndAbbreviations();
            _import_YoutubeDataRepository = import_YoutubeDataRepository;
            _storedSqlFunctionCaller = storedSqlFunctionCaller;
            _processed_YoutubeDataDTOBuilder = proccessed_YoutubeDataDTOBuilder;
            _youtubeTeamExtractor = youtubeTeamExtractor;
 
        }


        // 
        public async Task<List<Processed_YoutubeDataDTO>> TESTGetAllProcessedForEuAndNaTeams()
        {
            List <Import_YoutubeDataEntity> allvideos = await _storedSqlFunctionCaller.GetYoutubeDataEntitiesForWesternTeams();

            return  _processed_YoutubeDataDTOBuilder.BuildProcessed_YoutubeDataDTOList(allvideos);
        }

        public async Task TESTCheckExtractedTeams(List<Import_YoutubeDataEntity> import_YoutubeDataEntities, HashSet<string> distinctTeamNames)
        {
            await _teamNameService.PopulateTeamNamesAndAbbreviations();

            List<TeamnameDTO> teamNames = _teamNameService.GetTeamNamesAndAbbreviations();
            List<Import_YoutubeDataEntity> importYoutubeDataList = await _storedSqlFunctionCaller.GetYoutubeDataEntitiesForWesternTeams();
            List<Processed_YoutubeDataDTO> processedYoutubeDataList = _processed_YoutubeDataDTOBuilder.BuildProcessed_YoutubeDataDTOList(import_YoutubeDataEntities);





            

        }













    


   





















    }
}
