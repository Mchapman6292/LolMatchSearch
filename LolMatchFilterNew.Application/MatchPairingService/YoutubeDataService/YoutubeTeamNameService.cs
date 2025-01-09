using Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories;
using Domain.Interfaces.ApplicationInterfaces.IProcessed_YoutubeDataDTOBuilders;
using Domain.DTOs.YoutubeDataWithTeamsDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamExtractors;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamNameServices;

namespace Application.MatchPairingService.YoutubeDataService.YoutubeTeamNameServices
{
    public class YoutubeTeamNameService : IYoutubeTeamNameService
    {
        private readonly IAppLogger _appLogger;
        private readonly IImport_YoutubeDataRepository _import_YoutubeDataRepository;
        private readonly IStoredSqlFunctionCaller _storedSqlFunctionCaller;
        private readonly IProcessed_YoutubeDataDTOBuilder _processed_YoutubeDataDTOBuilder;
        private readonly IYoutubeTeamExtractor _youtubeTeamExtractor;
        


        public YoutubeTeamNameService(IAppLogger appLogger, IImport_YoutubeDataRepository import_YoutubeDataRepository, IStoredSqlFunctionCaller storedSqlFunctionCaller, IProcessed_YoutubeDataDTOBuilder processed_YoutubeDataDTOBuilder, IYoutubeTeamExtractor youtubeTeamExtractor)
        {
            _appLogger = appLogger;
            _import_YoutubeDataRepository = import_YoutubeDataRepository;
            _storedSqlFunctionCaller = storedSqlFunctionCaller;
            _processed_YoutubeDataDTOBuilder = processed_YoutubeDataDTOBuilder;
            _youtubeTeamExtractor = youtubeTeamExtractor;
        }

        public YoutubeDataWithTeamsDTO ExtractAndBuildProcessedDTO(Import_YoutubeDataEntity youtubeData)
        {

            List<string?> extractedTeams = _youtubeTeamExtractor.ExtractTeamNamesAroundVsKeyword(youtubeData.VideoTitle);

            string? team1 = extractedTeams.Count > 0 ? extractedTeams[0] : null;
            string? team2 = extractedTeams.Count > 1 ? extractedTeams[1] : null;


            return _processed_YoutubeDataDTOBuilder.BuildProcessedDTO(youtubeData, team1, team2);
        }

        public List<YoutubeDataWithTeamsDTO> BuildProcessed_YoutubeDataDTOList(List<Import_YoutubeDataEntity> youtubeDataEntities)
        {
            List<YoutubeDataWithTeamsDTO> processed = new List<YoutubeDataWithTeamsDTO>();

            int teamNullCount = 0;

            List<YoutubeDataWithTeamsDTO> YoutubeVideosWithNoMatch = new List<YoutubeDataWithTeamsDTO>();

            foreach (var video in youtubeDataEntities)
            {

                YoutubeDataWithTeamsDTO newDto = ExtractAndBuildProcessedDTO(video);

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


        public HashSet<string> GetDistinctYoutubeTeamNamesFromProcessed_YoutubeDataDTO(List<YoutubeDataWithTeamsDTO> processed_YoutubeDataDTOs)
        {
            HashSet<string> distinctTeamNames = new HashSet<string>();

            int distinctCount = 0;

            foreach (var dto in processed_YoutubeDataDTOs)
            {
                if (!string.IsNullOrEmpty(dto.Team1))
                {
                    distinctTeamNames.Add(dto.Team1);
                    distinctCount++;
                }
                if (!string.IsNullOrEmpty(dto.Team2))
                {
                    distinctTeamNames.Add(dto.Team2);
                    distinctCount++;
                }
            }
            _appLogger.Info($"Total number of distinct teams for {nameof(GetDistinctYoutubeTeamNamesFromProcessed_YoutubeDataDTO)}: {distinctCount}.");

            return distinctTeamNames;
        }


    }
}
