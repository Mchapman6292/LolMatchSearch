using Domain.DTOs.YoutubeDataWithTeamsDTOs;
using Domain.DTOs.YoutubeDataWithTeamsDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamNameServices
{
    public interface IYoutubeTeamNameService
    {
        HashSet<string> CONTROLLERGetAllDistinctNamesForWestern(List<Import_YoutubeDataEntity> youtubeData);
        YoutubeDataWithTeamsDTO ExtractAndBuildYoutubeDataWithTeamsDTO(Import_YoutubeDataEntity youtubeData);
        List<YoutubeDataWithTeamsDTO> BuildYoutubeDataWithTeamsDTOList(List<Import_YoutubeDataEntity> youtubeDataEntities);
        HashSet<string> GetDistinctYoutubeTeamNamesFromProcessed_YoutubeDataDTO(List<YoutubeDataWithTeamsDTO> processed_YoutubeDataDTOs);
    }
}
