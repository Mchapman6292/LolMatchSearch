using LolMatchFilterNew.Domain.Entities.Processed_TeamRenameEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamRenameRepositories
{
    public interface ITeamRenameRepository
    {
        Task<List<string>> GetCurrentTeamNamesAsync();
        Task<Dictionary<string, List<string>>> AddOriginalNameToNewNameAsync();
        Task<List<Processed_TeamRenameEntity>> GetAllTeamRenameValuesAsync();
    }
}
