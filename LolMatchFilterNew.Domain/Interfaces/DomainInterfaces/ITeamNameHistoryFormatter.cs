using LolMatchFilterNew.Domain.Entities.Processed_TeamNameHistoryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ITeamNameHistoryFormatters
{
    public interface ITeamNameHistoryFormatter
    {
        Dictionary<string, List<string>> FormatTeamHistoryToDict(List<Processed_TeamNameHistoryEntity> teamHistories);
        Dictionary<string, string> StandardizeDelimiters(List<Processed_TeamNameHistoryEntity> teamHistories);
    }
}
