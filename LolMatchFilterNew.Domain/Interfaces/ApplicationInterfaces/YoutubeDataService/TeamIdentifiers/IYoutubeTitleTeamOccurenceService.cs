using LolMatchFilterNew.Domain.DTOs.YoutubeTitleTeamOccurrenceDTOs;
using Domain.Interfaces.ApplicationInterfaces.YoutubeDataService.TeamIdentifiers.IYoutubeTitleTeamOccurenceServices;

namespace Domain.Interfaces.ApplicationInterfaces.YoutubeDataService.TeamIdentifiers.IYoutubeTitleTeamOccurenceServices
{
    public interface IYoutubeTitleTeamOccurenceService
    {
        void UpdateYoutubeTitle(YoutubeTitleTeamOccurenceDTO matchDTO, string title);
        void UpdateMatchingTeamNameIds(YoutubeTitleTeamOccurenceDTO matchResultDTO, string teamNameId, List<string> matches);
        void IncrementCount(YoutubeTitleTeamOccurenceDTO matchDTO, string countType, int increment);
        Dictionary<string, List<string>> GetTopTwoTeamMatches(YoutubeTitleTeamOccurenceDTO matchDTO);


    }
}
