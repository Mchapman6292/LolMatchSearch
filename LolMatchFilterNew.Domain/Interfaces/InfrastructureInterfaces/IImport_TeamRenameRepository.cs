using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRenameEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InfrastructureInterfaces.IImport_TeamRenameRepositories
{
    public interface IImport_TeamRenameRepository
    {
        Task<List<string>> GetCurrentTeamNamesAsync();
        Task<List<string>> TESTGet10CurrentTeamNamesAsync();
        Task<List<Import_TeamRenameEntity>> GetAllTeamRenameValuesAsync();
        Task<IEnumerable<Import_TeamRenameEntity>> GetTeamHistoryAsync(string teamName);
    }
}
