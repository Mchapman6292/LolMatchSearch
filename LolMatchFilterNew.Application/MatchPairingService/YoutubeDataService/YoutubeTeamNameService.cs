using Domain.DTOs.TeamnameDTOs;
using Domain.DTOs.YoutubeDataWithTeamsDTOs;
using Domain.DTOs.YoutubeTitleTeamNameOccurrenceCountDTOs;
using Domain.Interfaces.ApplicationInterfaces.IMatchDTOServices.IImport_TeamNameServices;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeDataWithTeamsDTOBuilders;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamNameServices;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamNameValidators;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamMatchCountFactories;
using Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers;
using Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamExtractors;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamNameFinders;

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
        private readonly IYoutubeTeamExtractor _youtubeTeamExtractor;
        private readonly IYoutubeTeamNameValidator _youtubeTeamNameValidator;
        private readonly IYoutubeTitleTeamMatchCountFactory _youtubeTitleTeamMatchCountFactory;
        private readonly IImport_TeamNameService _importTeamNameService;
        private readonly IYoutubeTitleTeamNameFinder _youtubeTitleTeamNameFinder;

        private List<YoutubeTitleTeamNameOccurrenceCountDTO> _youtubeTitleTeamMatchCounts { get; }



        public YoutubeTeamNameService(
         IAppLogger appLogger,
         IImport_YoutubeDataRepository import_YoutubeDataRepository,
         IStoredSqlFunctionCaller storedSqlFunctionCaller,
         IYoutubeDataWithTeamsDTOBuilder processed_YoutubeDataDTOBuilder,
         IYoutubeTeamExtractor youtubeTeamExtractor,
         IObjectLogger objectLogger,
         IYoutubeTeamNameValidator youtubeTeamNameValidator,
         IYoutubeTitleTeamMatchCountFactory youtubeTitleTeamMatchCountFactory,
         IImport_TeamNameService importTeamNameService,
         IYoutubeTitleTeamNameFinder youtubeTitleTeamNameFinder


         )
        {
            _appLogger = appLogger;
            _import_YoutubeDataRepository = import_YoutubeDataRepository;
            _storedSqlFunctionCaller = storedSqlFunctionCaller;
            _processed_YoutubeDataDTOBuilder = processed_YoutubeDataDTOBuilder;
            _youtubeTeamExtractor = youtubeTeamExtractor;
            _objectLogger = objectLogger;
            _youtubeTeamNameValidator = youtubeTeamNameValidator;
            _youtubeTitleTeamMatchCountFactory = youtubeTitleTeamMatchCountFactory;
            _importTeamNameService = importTeamNameService;
            _youtubeTitleTeamNameFinder = youtubeTitleTeamNameFinder;
            _youtubeTitleTeamMatchCounts = new List<YoutubeTitleTeamNameOccurrenceCountDTO>();
      

        }

        public void PopulateYoutubeTitleTeamMatchCountList(List<TeamNameDTO> teamNames)
        {
            foreach (var teamName in teamNames)
            {
                _youtubeTitleTeamMatchCounts.Add(_youtubeTitleTeamMatchCountFactory.CreateNewYoutubeTitleOccurenceDTO(teamName,string.Empty));
            }
        }

        public List<YoutubeTitleTeamNameOccurrenceCountDTO> ReturnYoutubeTitleTeamMatchCounts()
        {
            return _youtubeTitleTeamMatchCounts;
        }



        public YoutubeDataWithTeamsDTO MatchTeamsAroundVsKeyWord(Import_YoutubeDataEntity youtubeData)
        {

            List<string?> extractedTeams = _youtubeTeamExtractor.ExtractTeamNamesAroundVsKeyword(youtubeData.VideoTitle);


            _youtubeTeamExtractor.ExtractTeamNamesAroundVsKeyword(youtubeData.VideoTitle);

            string? team1 = extractedTeams.Count > 0 ? extractedTeams[0] : null;
            string? team2 = extractedTeams.Count > 1 ? extractedTeams[1] : null;

            return _processed_YoutubeDataDTOBuilder.BuildYoutubeDataWithTeamsDTO(youtubeData, team1, team2);
        }







        public List<YoutubeDataWithTeamsDTO> BuildYoutubeDataWithTeamsDTOList(List<Import_YoutubeDataEntity> youtubeDataEntities)
        {
            List<YoutubeDataWithTeamsDTO> processed = new List<YoutubeDataWithTeamsDTO>();

            int teamNullCount = 0;

            List<YoutubeDataWithTeamsDTO> YoutubeVideosWithNoMatch = new List<YoutubeDataWithTeamsDTO>();

            foreach (var video in youtubeDataEntities)
            {

                YoutubeDataWithTeamsDTO newDto = ExtractAndBuildYoutubeDataWithTeamsDTO(video);

                processed.Add(newDto);

                if (newDto.Team1 == null || newDto.Team1 == string.Empty)
                {
                    teamNullCount++;
                }
                if (newDto.Team2 == null || newDto.Team2 == string.Empty)
                {
                    teamNullCount++;
                }
                if (newDto.Team1 == null || newDto.Team1 == string.Empty && newDto.Team2 == null || newDto.Team2 == string.Empty)
                {
                    YoutubeVideosWithNoMatch.Add(newDto);

                }

            }

            if (YoutubeVideosWithNoMatch.Count > 0)
            {
                foreach (var video in YoutubeVideosWithNoMatch)
                {
                    _objectLogger.LogProcessedYoutubeDataDTO(video);
                }
            }

            _appLogger.Info($"NUMBER OF TEAM EXTRACTIONS FAILED: {teamNullCount}");
            return processed;

        }



        public YoutubeDataWithTeamsDTO ExtractAndBuildYoutubeDataWithTeamsDTO(Import_YoutubeDataEntity youtubeData)
        {

            List<string?> extractedTeams = _youtubeTeamExtractor.ExtractTeamNamesAroundVsKeyword(youtubeData.VideoTitle);



            string? team1 = extractedTeams.Count > 0 ? extractedTeams[0] : null;
            string? team2 = extractedTeams.Count > 1 ? extractedTeams[1] : null;



            return _processed_YoutubeDataDTOBuilder.BuildYoutubeDataWithTeamsDTO(youtubeData, team1, team2);
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




        public HashSet<string> CONTROLLERGetAllDistinctNamesForWestern(List<Import_YoutubeDataEntity> youtubeData)
        {
            List<YoutubeDataWithTeamsDTO> withTeams = BuildYoutubeDataWithTeamsDTOList(youtubeData);

            return GetDistinctYoutubeTeamNamesFromProcessed_YoutubeDataDTO(withTeams);
        }

    }
}
