using Domain.DTOs.YoutubeDataWithTeamsDTOs;
using Application.MatchPairingService.YoutubeDataService.YoutubeTitleTeamNameMatchResults;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;

namespace Domain.Interfaces.ApplicationInterfaces.IYoutubeTeamNameServices
{
    public interface IYoutubeTeamNameService
    {

        void PopulateYoutubeTitleTeamMatchCountList(List<Import_YoutubeDataEntity> teamNames);

        List<YoutubeTitleTeamNameMatchResult> ReturnYoutubeTitleTeamMatchCounts();
        HashSet<string> GetDistinctYoutubeTeamNamesFromProcessed_YoutubeDataDTO(List<YoutubeDataWithTeamsDTO> processed_YoutubeDataDTOs);
    }
}
