using LolMatchFilterNew.Domain.DTOs.YoutubeTitleTeamOccurrenceDTOs;

namespace Domain.Interfaces.ApplicationInterfaces.IYoutubeTitleTeamNameFinders
{
    public interface IYoutubeTitleTeamNameFinder
    {
        bool CheckNameMatch(string nameToCheck, string normalizedTitle);
        bool IsExactWordOccurrence(string text, string word);

    }
}
