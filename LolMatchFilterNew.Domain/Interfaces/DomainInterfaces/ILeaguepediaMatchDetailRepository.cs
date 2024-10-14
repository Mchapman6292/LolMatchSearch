using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaMatchDetailRepository
{
    public interface ILeaguepediaMatchDetailRepository
    {
        Task<int> BulkAddLeaguepediaMatchDetails(IEnumerable<LeaguepediaMatchDetailEntity> matchDetails);
        Task<int> DeleteAllRecordsAsync();
    }
}
