using Domain.DTOs.TeamnameDTOs;
using Domain.DTOs.YoutubeDataWithTeamsDTOs;
using Application.MatchPairingService.YoutubeDataService.YoutubeTitleTeamNameMatchResults;
using Domain.Interfaces.ApplicationInterfaces.IMatchDTOServices.IImport_TeamNameServices;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeDataWithTeamsDTOBuilders;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamNameServices;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamMatchCountFactories;
using Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers;
using Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamNameFinders;
using Google.Apis.YouTube.v3.Data;

namespace Application.MatchPairingService.YoutubeDataService.YoutubeTeamNameServices
{

    /// <summary>
    /// Processes a list of YouTube videos to extract team names and build structured DTOs.
    /// Workflow:
    /// 1. Calls methods from YoutubeTeamExtractor to extract teamNames from the YoutubeTitle
    /// 2. Source data taken from database using StoredSqlFunctionCaller GetYoutubeDataEntitiesForWesternTeamsAsync() to return List<Import_YoutubeDataEntity>
    /// 3. This is then converted to YoutubeDataWithTeamsDTO by calling methods from YoutubeTeamExtractor & YoutubeDataWithTeamsDTOBuilder.
    /// 3. Lastly GetDistinctYoutubeTeamNamesFromProcessed_YoutubeDataDTO is called to get all distinct Team names from the YoutubeDataWithTeamsDTO.



    public class YoutubeTeamNameService : IYoutubeTeamNameService
    {
        private readonly IAppLogger _appLogger;
        private readonly IObjectLogger _objectLogger;
        private readonly IImport_YoutubeDataRepository _import_YoutubeDataRepository;
        private readonly IStoredSqlFunctionCaller _storedSqlFunctionCaller;
        private readonly IYoutubeDataWithTeamsDTOBuilder _processed_YoutubeDataDTOBuilder;
        private readonly IYoutubeTitleTeamMatchCountFactory _youtubeTitleTeamMatchCountFactory;
        private readonly IImport_TeamNameService _importTeamNameService;
        private readonly IYoutubeTitleTeamNameFinder _youtubeTitleTeamNameFinder;

        private List<YoutubeTitleTeamNameMatchResult> _youtubeTitleTeamMatchCounts { get; }



        public YoutubeTeamNameService(
         IAppLogger appLogger,
         IImport_YoutubeDataRepository import_YoutubeDataRepository,
         IStoredSqlFunctionCaller storedSqlFunctionCaller,
         IYoutubeDataWithTeamsDTOBuilder processed_YoutubeDataDTOBuilder,
         IObjectLogger objectLogger,
         IYoutubeTitleTeamMatchCountFactory youtubeTitleTeamMatchCountFactory,
         IImport_TeamNameService importTeamNameService,
         IYoutubeTitleTeamNameFinder youtubeTitleTeamNameFinder


         )
        {
            _appLogger = appLogger;
            _import_YoutubeDataRepository = import_YoutubeDataRepository;
            _storedSqlFunctionCaller = storedSqlFunctionCaller;
            _processed_YoutubeDataDTOBuilder = processed_YoutubeDataDTOBuilder;
            _objectLogger = objectLogger;
            _youtubeTitleTeamMatchCountFactory = youtubeTitleTeamMatchCountFactory;
            _importTeamNameService = importTeamNameService;
            _youtubeTitleTeamNameFinder = youtubeTitleTeamNameFinder;
            _youtubeTitleTeamMatchCounts = new List<YoutubeTitleTeamNameMatchResult>();


        }

        public void PopulateYoutubeTitleTeamMatchCountList(List<Import_YoutubeDataEntity> teamNames)
        {
            foreach (var teamName in teamNames)
            {
                var dto = _youtubeTitleTeamMatchCountFactory.CreateNewYoutubeTitleOccurenceDTO(teamName.VideoTitle);
                _youtubeTitleTeamMatchCounts.Add(dto);
            }
            _appLogger.Info($"Total count for _youtubeTitleTeamMatchCounts: {_youtubeTitleTeamMatchCounts.Count()}.");
        }

        public List<YoutubeTitleTeamNameMatchResult> ReturnYoutubeTitleTeamMatchCounts()
        {
            if (!_youtubeTitleTeamMatchCounts.Any())
            {
                throw new InvalidOperationException($"YouTube title team match counts list has not been initialized. This list should be populated during service initialization by calling.{nameof(PopulateYoutubeTitleTeamMatchCountList)}.");
            }
            return _youtubeTitleTeamMatchCounts;
        }





        public HashSet<string> GetDistinctYoutubeTeamNamesFromProcessed_YoutubeDataDTO(List<YoutubeDataWithTeamsDTO> YoutubeDataWithTeamsDTO)
        {
            HashSet<string> distinctTeamNames = new HashSet<string>();



            foreach (var dto in YoutubeDataWithTeamsDTO)
            {
                if (!string.IsNullOrEmpty(dto.Team1))
                {
                    distinctTeamNames.Add(dto.Team1);
                }
                if (!string.IsNullOrEmpty(dto.Team2))
                {
                    distinctTeamNames.Add(dto.Team2);
                }
            }
            _appLogger.Info($"Total number of distinct teams for {nameof(GetDistinctYoutubeTeamNamesFromProcessed_YoutubeDataDTO)}: {distinctTeamNames.Count}.");

            return distinctTeamNames;
        }










    }
}
