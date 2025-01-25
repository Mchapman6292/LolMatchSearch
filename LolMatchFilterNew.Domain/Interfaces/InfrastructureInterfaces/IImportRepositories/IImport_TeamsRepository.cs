using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamsTableEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InfrastructureInterfaces.IImportRepositories.IImport_TeamsRepositories
{
    public interface IImport_TeamsRepository
    {
        Task<List<Import_TeamsTableEntity>> JoinTeamsNameOnTeamNameLongName();
    }
}
