using LolMatchFilterNew.Domain.Entities.TeamNameHistoryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamRenameToHistoryMappers
{
    public interface ITeamRenameToHistoryMapper
    {
        Task<List<TeamNameHistoryEntity>> MapTeamRenameToHistoryAsync();
    }
}
