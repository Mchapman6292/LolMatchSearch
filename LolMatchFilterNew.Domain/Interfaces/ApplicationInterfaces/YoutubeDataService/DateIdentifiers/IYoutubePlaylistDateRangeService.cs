using Domain.DTOs.PlayListDateRangeDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;

namespace Domain.Interfaces.ApplicationInterfaces.YoutubeDataService.DateIdentifiers.IYoutubePlaylistDateRangeServices
{
    public interface IYoutubePlaylistDateRangeService
    {
        Task PopulateTournmanetDateRanges(List<Import_YoutubeDataEntity> youtubeEntities);

        List<PlayListDateRangeResult> ReturnTournamentDateRanges();
    }
}
