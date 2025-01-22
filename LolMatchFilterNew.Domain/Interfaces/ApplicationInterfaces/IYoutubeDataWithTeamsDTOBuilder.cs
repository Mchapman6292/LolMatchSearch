using Domain.DTOs.YoutubeDataWithTeamsDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;

namespace Domain.Interfaces.ApplicationInterfaces.IYoutubeDataWithTeamsDTOBuilders
{
    public interface IYoutubeDataWithTeamsDTOBuilder
    {
        YoutubeDataWithTeamsDTO BuildYoutubeDataWithTeamsDTO(Import_YoutubeDataEntity youtubeDataEntity, string team1, string team2);



    }
}
