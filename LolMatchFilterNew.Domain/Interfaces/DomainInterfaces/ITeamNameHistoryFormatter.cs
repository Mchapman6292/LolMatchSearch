using LolMatchFilterNew.Domain.Entities.TeamNameHistoryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ITeamNameHistoryFormatters
{
    public interface ITeamNameHistoryFormatter
    {
        Dictionary<string, List<string>> FormatTeamHistoryToDict(List<TeamNameHistoryEntity> teamHistories);
        Dictionary<string, string> StandardizeDelimiters(List<TeamNameHistoryEntity> teamHistories);
    }
}
