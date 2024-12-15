using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_TeamNameHistoryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamRenameToHistoryMappers
{
    public interface ITeamRenameToHistoryMapper
    {
        Task<List<Processed_TeamNameHistoryEntity>> MapTeamRenameToHistoryAsync();
        Task<Dictionary<string, List<string>>> AddOriginalNameToNewNameAsync();
        Task<Dictionary<string, List<string>>> TESTAddOriginalNameToNewNameAsync();
    }
}
