using Domain.DTOs.TeamnameDTOs;
using Domain.DTOs.YoutubeTitleTeamNameOccurrenceCountDTOs;

namespace Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamMatchCountFactories
{
    public interface IYoutubeTitleTeamMatchCountFactory
    {
        YoutubeTitleTeamNameOccurrenceCountDTO CreateNewYoutubeTitleOccurenceDTO(TeamNameDTO? teamNameDTO, string youtubeTitle, int longNameMatch = 0, int mediumNameMatch = 0, int shortNameMatch = 0, List<string> matchingInputs = null);

        void UpdateTeamNameDtoForMatch(YoutubeTitleTeamNameOccurrenceCountDTO matchCount, TeamNameDTO? newTeamDto);

        void UpdateYoutubeTitle(YoutubeTitleTeamNameOccurrenceCountDTO dto, string title);


    }
}
