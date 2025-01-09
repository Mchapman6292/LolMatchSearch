using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchServiceControllers;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchComparisonResultBuilders;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamExtractors;
using Domain.DTOs.TeamnameDTOs;
using Domain.Interfaces.ApplicationInterfaces.ITeamnameDTOBuilders;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;
using Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers;
using Domain.Interfaces.ApplicationInterfaces.ITeamNameValidators;
using Application.MatchPairingService.YoutubeDataService.Processed_YoutubeDataDTOBuilder;
using Domain.Interfaces.ApplicationInterfaces.IProcessed_YoutubeDataDTOBuilders;
using Domain.DTOs.Processed_YoutubeDataDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;

namespace Application.MatchPairingService.MatchComparisonResultService.MatchComparisonControllers
{
    public class MatchComparisonController : IMatchComparisonController
    {
        private readonly IAppLogger _appLogger;
        private readonly IMatchComparisonResultBuilder _matchBuilder;
        private readonly IYoutubeTeamExtractor _youtubeTeamExtractor;
        private readonly ITeamnameDTOBuilder _teamnameDTOBuilder;
        private readonly IStoredSqlFunctionCaller _sqlFunctionCaller;
        private readonly IImport_YoutubeDataRepository _import_YoutubeDataRepository;
        private readonly IProcessed_YoutubeDataDTOBuilder _processed_YoutubeDataDTOBuilder;
        private readonly List<TeamnameDTO> _KnownTeamNamesAndAbbreviations;





        public MatchComparisonController(

            IAppLogger appLogger,
            IMatchComparisonResultBuilder matchComparisonResultBuilder,
            ITeamnameDTOBuilder teamnameDTOBuilder,
            IImport_YoutubeDataRepository import_YoutubeDataRepository,
            IStoredSqlFunctionCaller storedSqlFunctionCaller,
            IProcessed_YoutubeDataDTOBuilder youtubeDataDTOBuilder,
            IYoutubeTeamExtractor youtubeTeamExtractor
        )
        {
            _appLogger = appLogger;
            _matchBuilder = matchComparisonResultBuilder;

            _teamnameDTOBuilder = teamnameDTOBuilder;
            _KnownTeamNamesAndAbbreviations = _teamnameDTOBuilder.GetTeamNamesAndAbbreviations();
            _import_YoutubeDataRepository = import_YoutubeDataRepository;
            _sqlFunctionCaller = storedSqlFunctionCaller;
            _processed_YoutubeDataDTOBuilder = youtubeDataDTOBuilder;
            _youtubeTeamExtractor = youtubeTeamExtractor;
        }


        // 
        public async Task<List<Processed_YoutubeDataDTO>> TESTGetAllProcessedForEuAndNaTeams()
        {
            List <Import_YoutubeDataEntity> allvideos = await _import_YoutubeDataRepository.GetAllVideoDataForEuAndNaTeamsByPlaylistId();

            return await ExtractAndBuildAllProcessedDTO(allvideos);
        }

        public async Task TESTCheckExtractedTeams(List<Import_YoutubeDataEntity> import_YoutubeDataEntities, HashSet<string> distinctTeamNames)
        {
            
        }













        public async Task<Processed_YoutubeDataDTO> ExtractAndBuildProcessedDTO(Import_YoutubeDataEntity youtubeData)
        {

            List<string?> extractedTeams =  _youtubeTeamExtractor.ExtractTeamNamesAroundVsKeyword(youtubeData.VideoTitle);

            string? team1 = extractedTeams.Count > 0 ? extractedTeams[0] : null;
            string? team2 = extractedTeams.Count > 1 ? extractedTeams[1] : null;


            return _processed_YoutubeDataDTOBuilder.BuildProcessedDTO(youtubeData, team1, team2);
        }



        public async Task<List<Processed_YoutubeDataDTO>> ExtractAndBuildAllProcessedDTO(List<Import_YoutubeDataEntity> youtubeDataList)
        {
            List<Processed_YoutubeDataDTO> processed = new List<Processed_YoutubeDataDTO>();

            int teamNullCount = 0;

            List<Processed_YoutubeDataDTO> YoutubeVideosWithNoMatch = new List<Processed_YoutubeDataDTO>();

            foreach (var video in youtubeDataList)
            {


                Processed_YoutubeDataDTO newDto = await ExtractAndBuildProcessedDTO(video);

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
                    Console.WriteLine($"Playlist title {video.PlaylistTitle}, title: {video.VideoTitle}.");
                }
            }

            _appLogger.Info($"NUMBER OF TEAM EXTRACTIONS FAILED: {teamNullCount}");
            return processed;

        }























    }
}
