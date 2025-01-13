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
using Domain.Interfaces.InfrastructureInterfaces.IObjectLoggers;
using Application.MatchPairingService.ScoreboardGameService.MatchDTOServices.TeamNameServices.Import_TeamNameServices;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamNameFinders;
using Domain.DTOs.YoutubeTitleTeamNameOccurrenceCountDTOs;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamMatchCountFactories;

namespace Application.MatchPairingService.MatchComparisonResultService.MatchComparisonControllers
{
    public class MatchComparisonController : IMatchComparisonController
    {
        private readonly IAppLogger _appLogger;
        private readonly IObjectLogger _objectLogger;
        private readonly IMatchComparisonResultBuilder _matchBuilder;
        private readonly IYoutubeTeamExtractor _youtubeTeamExtractor;
        private readonly ITeamNameDTOBuilder _teamnameDTOBuilder;
        private readonly IStoredSqlFunctionCaller _storedSqlFunctionCaller;
        private readonly IImport_YoutubeDataRepository _import_YoutubeDataRepository;
        private readonly IYoutubeDataWithTeamsDTOBuilder _processed_YoutubeDataDTOBuilder;
        private readonly IImport_TeamNameService _import_TeamNameService;
        private readonly IYoutubeTeamNameService _youtubeTeamNameService;
        private readonly IYoutubeTeamNameValidator _youtubeTeamNameValidator;
        private readonly IYoutubeTitleTeamNameFinder _youtubeTitleTeamNameFinder;
        private readonly IImport_TeamNameService _importTeamNameService;
        private readonly IYoutubeTitleTeamMatchCountFactory _youtubeTitleTeamMatchCountFactory;




        public MatchComparisonController(

            IAppLogger appLogger,
            IObjectLogger objectLogger,
            IMatchComparisonResultBuilder matchComparisonResultBuilder,
            ITeamNameDTOBuilder teamnameDTOBuilder,
            IImport_YoutubeDataRepository import_YoutubeDataRepository,
            IStoredSqlFunctionCaller storedSqlFunctionCaller,
            IYoutubeDataWithTeamsDTOBuilder proccessed_YoutubeDataDTOBuilder,
            IImport_TeamNameService import_TeamnNameService,
            IYoutubeTeamExtractor youtubeTeamExtractor,
            IYoutubeTeamNameService youtubeTeamNameService,

            IYoutubeTeamNameValidator youtubeTeamNameValidator,
            IYoutubeTitleTeamMatchCountFactory youtubeTitleTeamMatchCountFactory
            

        )
        {
            _appLogger = appLogger;
            _objectLogger = objectLogger;
            _matchBuilder = matchComparisonResultBuilder;

            _teamnameDTOBuilder = teamnameDTOBuilder;
            _import_TeamNameService = import_TeamnNameService;
            _import_YoutubeDataRepository = import_YoutubeDataRepository;
            _storedSqlFunctionCaller = storedSqlFunctionCaller;
            _processed_YoutubeDataDTOBuilder = proccessed_YoutubeDataDTOBuilder;
            _youtubeTeamExtractor = youtubeTeamExtractor;
            _youtubeTeamNameService = youtubeTeamNameService;
            _youtubeTeamNameValidator = youtubeTeamNameValidator;
            _youtubeTitleTeamMatchCountFactory = youtubeTitleTeamMatchCountFactory;


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
            List<string> matchingTeamList = new List<string>();

            int noMatch = 0;
            List<string> noMatchesList = new List<string>();

            foreach (var teamName in distinctTeamNames)
            {
                if (_youtubeTeamNameValidator.ValidateTeamName(teamName, import_TeamNameAllNames))
                {
                    matchingTeams++;
                    matchingTeamList.Add(teamName);
                }
                else
                {
                    noMatch++;
                    noMatchesList.Add(teamName);
                }
            }
            _appLogger.Debug($"Matching teams: {matchingTeams}, No matches {noMatch}.");
            _objectLogger.LogListForCONTROLLERValidateWesternMatches(noMatchesList);

        }


        public YoutubeDataWithTeamsDTO ExtractAndBuildYoutubeDataWithTeamsDTO(Import_YoutubeDataEntity youtubeData)
        {
            List<TeamNameDTO> importTeamNameAllNames = _importTeamNameService.ReturnImport_TeamNameAllNames();

            List<YoutubeTitleTeamNameOccurrenceCountDTO> youtubeTitleTeamMatchCounts = _youtubeTeamNameService.ReturnYoutubeTitleTeamMatchCounts();

            string finalTeam1 = string.Empty;
            string finalTeam2 = string.Empty;

            List<string?> extractedTeams = _youtubeTeamExtractor.ExtractTeamNamesAroundVsKeyword(youtubeData.VideoTitle);


            string? team1 = extractedTeams.Count > 0 ? extractedTeams[0] : null;
            string? team2 = extractedTeams.Count > 1 ? extractedTeams[1] : null;

            bool teamOneMatchFound = false;
            bool teamTwoMatchFound = false;

            if (team1 != null)
            {
                teamOneMatchFound = _youtubeTeamNameValidator.ValidateTeamName(team1, importTeamNameAllNames);
            }

            if (team2 != null)
            {
                teamTwoMatchFound = _youtubeTeamNameValidator.ValidateTeamName(team2, importTeamNameAllNames);
            }

            if (teamOneMatchFound == false || teamTwoMatchFound == false)
            {
                _youtubeTitleTeamNameFinder.IncrementAllCountsForMatchesInTitle(youtubeData.VideoTitle, youtubeTitleTeamMatchCounts);
            }

            return _processed_YoutubeDataDTOBuilder.BuildYoutubeDataWithTeamsDTO(youtubeData, team1, team2);
        }


        public void TESTCreateTenYoutubeTitleOccurence(List<Import_YoutubeDataEntity> youtubeEntities)
        {
   
            List<YoutubeTitleTeamNameOccurrenceCountDTO> youtubeTitleTeamMatchCounts = _youtubeTeamNameService.ReturnYoutubeTitleTeamMatchCounts();
            List<YoutubeTitleTeamNameOccurrenceCountDTO> tenCounts = youtubeTitleTeamMatchCounts.Take(10).ToList();
            List<Import_YoutubeDataEntity> tenYoutubeEntities = youtubeEntities.Take(10).ToList();

            for (int i = 0; i < tenCounts.Count && i < tenYoutubeEntities.Count; i++)
            {
                _youtubeTitleTeamMatchCountFactory.UpdateYoutubeTitle(tenCounts[i], tenYoutubeEntities[i].VideoTitle);
            }
            foreach (var youtubeDto in tenYoutubeEntities)
            {
                _youtubeTitleTeamNameFinder.IncrementAllCountsForMatchesInTitle(youtubeDto.VideoTitle, tenCounts);
            }
            foreach (var countDto in tenCounts)
            {
                _objectLogger.LogYoutubeTitleTeamNameOccurrenceCountDTO(countDto);
            }
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




        public void TESTIncrementMatchesCount(List<YoutubeTitleTeamNameOccurrenceCountDTO> youtubeTitleDTOs)
        {
            var first10Items = youtubeTitleDTOs.Take(10);
            foreach (var item in first10Items)
            {

            }
        }




    }
}













    


   




















