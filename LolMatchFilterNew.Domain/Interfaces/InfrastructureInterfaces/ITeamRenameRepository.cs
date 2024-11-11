using LolMatchFilterNew.Domain.Entities.TeamRenamesEntities;
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
        Task<List<TeamRenameEntity>> GetAllTeamRenameValuesAsync();
    }
}
