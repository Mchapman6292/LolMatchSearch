using Domain.DTOs.YoutubeDataWithTeamsDTOs;
using LolMatchFilterNew.Domain.DTOs.YoutubeTitleTeamOccurrenceDTOs; 
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;

namespace Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamNameServices
{
    public interface IYoutubeTeamNameService
    {

        void PopulateYoutubeTitleTeamNameOccurrencesList(List<Import_YoutubeDataEntity> teamNames);

        List<YoutubeTitleTeamOccurenceDTO> ReturnYoutubeTitleTeamMatchCounts();
        HashSet<string> GetDistinctYoutubeTeamNamesFromProcessed_YoutubeDataDTO(List<YoutubeDataWithTeamsDTO> processed_YoutubeDataDTOs);
    }
}
