using Domain.DTOs.YoutubeDataWithTeamsDTOs;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeDataWithTeamsDTOBuilders;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;

namespace Application.MatchPairingService.YoutubeDataService.YoutubeDataWithTeamsDTOBuilders;

public class YoutubeDataWithTeamsDTOBuilder : IYoutubeDataWithTeamsDTOBuilder
{
    private readonly IAppLogger _appLogger;

    public YoutubeDataWithTeamsDTOBuilder(IAppLogger appLogger)
    {
        _appLogger = appLogger;
    }


    public YoutubeDataWithTeamsDTO BuildYoutubeDataWithTeamsDTO(Import_YoutubeDataEntity youtubeDataEntity, string? team1, string? team2)
    {
        return new YoutubeDataWithTeamsDTO
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






