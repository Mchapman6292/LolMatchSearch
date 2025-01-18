using Domain.DTOs.YoutubeDataWithTeamsDTOs;
using Application.MatchPairingService.YoutubeDataService.YoutubeTitleTeamNameMatchResults;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;

namespace Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamNameServices
{
    public interface IYoutubeTeamNameService
    {
        HashSet<string> CONTROLLERGetAllDistinctNamesForWestern(List<Import_YoutubeDataEntity> youtubeData);

        void PopulateYoutubeTitleTeamMatchCountList(List<Import_YoutubeDataEntity> teamNames);

        List<YoutubeTitleTeamNameMatchResult> ReturnYoutubeTitleTeamMatchCounts();
        YoutubeDataWithTeamsDTO ExtractAndBuildYoutubeDataWithTeamsDTO(Import_YoutubeDataEntity youtubeData);
        List<YoutubeDataWithTeamsDTO> BuildYoutubeDataWithTeamsDTOList(List<Import_YoutubeDataEntity> youtubeDataEntities);
        HashSet<string> GetDistinctYoutubeTeamNamesFromProcessed_YoutubeDataDTO(List<YoutubeDataWithTeamsDTO> processed_YoutubeDataDTOs);
    }
}
