using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_Teamnames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InfrastructureInterfaces.IImport_TeamnameRepositories
{
    public interface IImport_TeamnameRepository
    {
        Task<List<Import_TeamnameEntity>> GetAllTeamnamesAsync();
    }
}
