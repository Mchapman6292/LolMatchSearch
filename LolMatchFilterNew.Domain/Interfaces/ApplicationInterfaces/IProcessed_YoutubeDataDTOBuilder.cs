using Domain.DTOs.YoutubeDataWithTeamsDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.ApplicationInterfaces.IProcessed_YoutubeDataDTOBuilders
{
    public interface IProcessed_YoutubeDataDTOBuilder
    {
        YoutubeDataWithTeamsDTO BuildProcessedDTO(Import_YoutubeDataEntity youtubeDataEntity, string team1, string team2);



    }
}
