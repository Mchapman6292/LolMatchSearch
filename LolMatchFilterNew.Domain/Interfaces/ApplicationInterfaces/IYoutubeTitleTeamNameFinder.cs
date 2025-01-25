using LolMatchFilterNew.Domain.DTOs.YoutubeTitleTeamOccurrenceDTOs;

namespace Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamNameFinders
{
    public interface IYoutubeTitleTeamNameFinder
    {
        void ProcessYoutubeTitle(YoutubeTitleTeamOccurenceDTO occurrenceDTO);

    }
}
