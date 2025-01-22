using Domain.DTOs.PlayListDateRangeDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;

namespace Domain.Interfaces.ApplicationInterfaces.IDTOBuilders.PlayListDateRangeServices
{
    public interface IPlayListDateRangeService
    {
        void PopulateGamesWithinPlaylistDates(List<Import_YoutubeDataEntity> youtubeEntities);

        Task UpdateGamesWithinYoutubePlaylistDatesWithMatchesAsync();

        List<PlayListDateRangeResult> ReturngamesWithinYoutubePlaylistDates();
    }
}
