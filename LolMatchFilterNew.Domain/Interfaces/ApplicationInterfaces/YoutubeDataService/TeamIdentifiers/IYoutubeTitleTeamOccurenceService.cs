using Domain.Enums.TeamNameTypes;
using LolMatchFilterNew.Domain.DTOs.YoutubeTitleTeamOccurrenceDTOs;

namespace Domain.Interfaces.ApplicationInterfaces.YoutubeDataService.TeamIdentifiers.IYoutubeTitleTeamOccurrenceServices
{
    public interface IYoutubeTitleTeamOccurenceService
    {


        void UpdateYoutubeTitle(YoutubeTitleTeamOccurenceDTO matchDTO, string title);
        void UpdateMatchingTeamNameIds(YoutubeTitleTeamOccurenceDTO matchResultDTO, string teamNameId, List<(TeamNameType, string)> matches);
        void IncrementCount(YoutubeTitleTeamOccurenceDTO matchResultDTO, string countType, int increment);
        Dictionary<string, List<(TeamNameType, string)>> GetTeamIdsWithHighestOccurences(YoutubeTitleTeamOccurenceDTO matchDTO);

        YoutubeTitleTeamOccurenceDTO TallyTeamNameOccurrences(YoutubeTitleTeamOccurenceDTO occurrenceDTO);
        void PopulateTeamIdsWithMostMatches(YoutubeTitleTeamOccurenceDTO occurrenceDTO, Dictionary<string, List<(TeamNameType, string)>> teamIdWithMatches);


        void TESTPopulateTeamIdsWithMostMatches(YoutubeTitleTeamOccurenceDTO occurrenceDTO,
    Dictionary<string, List<(TeamNameType, string)>> teamIdWithMatches);


    }
}
