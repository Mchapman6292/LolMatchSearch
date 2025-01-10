using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchServiceControllers;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchComparisonResultBuilders;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamExtractors;
using Domain.DTOs.TeamnameDTOs;
using Domain.Interfaces.ApplicationInterfaces.ITeamNameDTOBuilders;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;
using Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamNameValidators;
using Application.MatchPairingService.YoutubeDataService.YoutubeDataWithTeamsDTOBuilders;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeDataWithTeamsDTOBuilders;
using Domain.DTOs.YoutubeDataWithTeamsDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using Domain.Interfaces.ApplicationInterfaces.IMatchDTOServices.IImport_TeamNameServices;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamNameServices;
using Application.MatchPairingService.YoutubeDataService.YoutubeTeamNameValidators;

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
        private readonly IYoutubeDataWithTeamsDTOBuilder _processed_YoutubeDataDTOBuilder;
        private readonly IImport_TeamNameService _import_TeamNameService;
        private readonly IYoutubeTeamNameService _youtubeTeamNameService;
        private readonly IYoutubeTeamNameValidator _youtubeTeamNameValidator;





        public MatchComparisonController(

            IAppLogger appLogger,
            IMatchComparisonResultBuilder matchComparisonResultBuilder,
            ITeamNameDTOBuilder teamnameDTOBuilder,
            IImport_YoutubeDataRepository import_YoutubeDataRepository,
            IStoredSqlFunctionCaller storedSqlFunctionCaller,
            IYoutubeDataWithTeamsDTOBuilder proccessed_YoutubeDataDTOBuilder,
            IImport_TeamNameService import_TeamnNameService,
            IYoutubeTeamExtractor youtubeTeamExtractor,
            IYoutubeTeamNameService youtubeTeamNameService,
            IYoutubeTeamNameValidator youtubeTeamNameValidator

        )
        {
            _appLogger = appLogger;
            _matchBuilder = matchComparisonResultBuilder;

            _teamnameDTOBuilder = teamnameDTOBuilder;
            _import_TeamNameService = import_TeamnNameService;
            _import_YoutubeDataRepository = import_YoutubeDataRepository;
            _storedSqlFunctionCaller = storedSqlFunctionCaller;
            _processed_YoutubeDataDTOBuilder = proccessed_YoutubeDataDTOBuilder;
            _youtubeTeamExtractor = youtubeTeamExtractor;
            _youtubeTeamNameService = youtubeTeamNameService;
            _youtubeTeamNameValidator = youtubeTeamNameValidator;


        }


        // 
        public async Task<List<YoutubeDataWithTeamsDTO>> TESTGetAllProcessedForEuAndNaTeams()
        {
            List<Import_YoutubeDataEntity> allvideos = await _storedSqlFunctionCaller.GetYoutubeDataEntitiesForWesternTeamsAsync();

            return _youtubeTeamNameService.BuildYoutubeDataWithTeamsDTOList(allvideos);
        }

        




        public async Task TESTCheckExtractedTeamsAsync(List<Import_YoutubeDataEntity> import_YoutubeDataEntities, HashSet<string> distinctTeamNames)
        {
            await _import_TeamNameService.PopulateImport_TeamNameAllNames();

            List<TeamNameDTO> import_TeamNameAllNames = _import_TeamNameService.ReturnImport_TeamNameAllNames();
            List<YoutubeDataWithTeamsDTO> processedYoutubeDataList = _youtubeTeamNameService.BuildYoutubeDataWithTeamsDTOList(import_YoutubeDataEntities);

            int matchingTeams = 0;
            int noMatch = 0;



            foreach (string teamName in _youtubeTeamNameService.GetDistinctYoutubeTeamNamesFromProcessed_YoutubeDataDTO(processedYoutubeDataList))
            {
                if (_youtubeTeamNameValidator.ValidateTeamName(teamName, import_TeamNameAllNames))
                {
                    matchingTeams++;
                }
                else
                {
                    noMatch++;
                }
            }

            _appLogger.Debug($"Matching teams: {matchingTeams}, No matches {noMatch}.");
        }



        public void CONTROLLERValidateWesternMatches(HashSet<string> distinctTeamNames, List<TeamNameDTO> import_TeamNameAllNames)
        {

            int matchingTeams = 0;
            int noMatch = 0;

            foreach (var teamName in distinctTeamNames)
            {
                if (_youtubeTeamNameValidator.ValidateTeamName(teamName, import_TeamNameAllNames))
                {
                    matchingTeams++;
                }
                else
                {
                    noMatch++;
                }
            }
            _appLogger.Debug($"Matching teams: {matchingTeams}, No matches {noMatch}.");
        }



    }
}













    


   




















