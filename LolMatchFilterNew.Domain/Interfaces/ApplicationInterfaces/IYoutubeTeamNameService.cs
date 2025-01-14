using Domain.DTOs.YoutubeDataWithTeamsDTOs;
using Domain.DTOs.YoutubeTitleTeamNameOccurrenceCountDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;

namespace Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamNameServices
{
    public interface IYoutubeTeamNameService
    {
        HashSet<string> CONTROLLERGetAllDistinctNamesForWestern(List<Import_YoutubeDataEntity> youtubeData);

        void CONTROLLERUpdateAllYoutubeTitleDTO(List<YoutubeTitleTeamNameOccurrenceCountDTO> youtubeTitleTeamDTOs);

        void PopulateYoutubeTitleTeamMatchCountList(List<Import_YoutubeDataEntity> teamNames);

        List<YoutubeTitleTeamNameOccurrenceCountDTO> ReturnYoutubeTitleTeamMatchCounts();
        YoutubeDataWithTeamsDTO ExtractAndBuildYoutubeDataWithTeamsDTO(Import_YoutubeDataEntity youtubeData);
        List<YoutubeDataWithTeamsDTO> BuildYoutubeDataWithTeamsDTOList(List<Import_YoutubeDataEntity> youtubeDataEntities);
        HashSet<string> GetDistinctYoutubeTeamNamesFromProcessed_YoutubeDataDTO(List<YoutubeDataWithTeamsDTO> processed_YoutubeDataDTOs);
    }
}
