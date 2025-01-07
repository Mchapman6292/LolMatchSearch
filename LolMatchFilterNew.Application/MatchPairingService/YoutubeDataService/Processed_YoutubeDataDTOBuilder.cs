﻿using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using Domain.DTOs.Processed_YoutubeDataDTOs;
using Domain.Interfaces.ApplicationInterfaces.IProcessed_YoutubeDataDTOBuilders;

namespace Application.MatchPairingService.YoutubeDataService.Processed_YoutubeDataDTOBuilder
{
    public class Processed_YoutubeDataDTOBuilder : IProcessed_YoutubeDataDTOBuilder
    {
        

        public Processed_YoutubeDataDTOBuilder()
        {

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







        
    }
}
