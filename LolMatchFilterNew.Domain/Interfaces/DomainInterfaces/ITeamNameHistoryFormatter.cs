using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_TeamNameHistoryEntities;

namespace LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ITeamNameHistoryFormatters
{
    public interface ITeamNameHistoryFormatter
    {
        Dictionary<string, List<string>> FormatTeamHistoryToDict(List<Processed_TeamNameHistoryEntity> teamHistories);
        Dictionary<string, string> StandardizeDelimiters(List<Processed_TeamNameHistoryEntity> teamHistories);
    }
}
