using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using Domain.DTOs.Processed_YoutubeDataDTOs;
using Domain.Interfaces.ApplicationInterfaces.IProcessed_YoutubeDataDTOBuilders;
using System.Drawing.Text;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamExtractors;

namespace Application.MatchPairingService.YoutubeDataService.Processed_YoutubeDataDTOBuilder
{
public class Processed_YoutubeDataDTOBuilder : IProcessed_YoutubeDataDTOBuilder
    {
        private readonly IAppLogger _appLogger;
        private readonly IYoutubeTeamExtractor _youtubeTeamExtractor;

        public Processed_YoutubeDataDTOBuilder(IAppLogger appLogger, IYoutubeTeamExtractor youtubeTeamExtractor)
        {
            _appLogger = appLogger;
            _youtubeTeamExtractor = youtubeTeamExtractor;



        }


        public Processed_YoutubeDataDTO BuildProcessedDTO(Import_YoutubeDataEntity youtubeDataEntity, string? team1, string? team2)
        {
            return new Processed_YoutubeDataDTO
            {
                YoutubeVideoId = youtubeDataEntity.YoutubeVideoId,
                VideoTitle = youtubeDataEntity.VideoTitle,
                PlaylistId = youtubeDataEntity.PlaylistId,
                PlaylistTitle = youtubeDataEntity.PlaylistTitle,
                PublishedAtUtc = youtubeDataEntity.PublishedAt_utc,
                YoutubeResultHyperlink = youtubeDataEntity.YoutubeResultHyperlink,
                ThumbnailUrl = youtubeDataEntity.ThumbnailUrl,
                Team1 = team1 ?? string.Empty,
                Team2 = team2 ?? string.Empty
            };
        }


        public List<Processed_YoutubeDataDTO> BuildProcessed_YoutubeDataDTOList(List<Import_YoutubeDataEntity> youtubeDataEntities) 
        {
            List<Processed_YoutubeDataDTO> processed = new List<Processed_YoutubeDataDTO>();

            int teamNullCount = 0;

            List<Processed_YoutubeDataDTO> YoutubeVideosWithNoMatch = new List<Processed_YoutubeDataDTO>();

            foreach (var video in youtubeDataEntities)
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
