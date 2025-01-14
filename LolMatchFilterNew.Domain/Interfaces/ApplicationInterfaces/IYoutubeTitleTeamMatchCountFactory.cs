using Domain.DTOs.TeamnameDTOs;
using Domain.DTOs.YoutubeTitleTeamNameOccurrenceCountDTOs;

namespace Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamMatchCountFactories
{
    public interface IYoutubeTitleTeamMatchCountFactory
    {
        public YoutubeTitleTeamNameOccurrenceCountDTO CreateNewYoutubeTitleOccurenceDTO(string youtubeTitle, int longNameCount = 0, int mediumNameCount = 0, int shortNameCount = 0, int matchingInputsCount = 0, List<string> matchingInputs = null);


    }
}
