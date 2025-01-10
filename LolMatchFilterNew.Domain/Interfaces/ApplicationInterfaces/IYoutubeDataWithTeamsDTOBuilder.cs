using Domain.DTOs.YoutubeDataWithTeamsDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeDataWithTeamsDTOBuilders;
using Domain.Interfaces.ApplicationInterfaces.IYoutubeDataWithTeamsDTOBuilders;

namespace Domain.Interfaces.ApplicationInterfaces.IYoutubeDataWithTeamsDTOBuilders
{
    public interface IYoutubeDataWithTeamsDTOBuilder
    {
        YoutubeDataWithTeamsDTO BuildYoutubeDataWithTeamsDTO(Import_YoutubeDataEntity youtubeDataEntity, string team1, string team2);



    }
}
