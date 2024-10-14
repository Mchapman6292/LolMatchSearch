using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaApiMappers
{
    public interface ILeaguepediaApiMapper
    {
        Task<IEnumerable<LeaguepediaMatchDetailEntity>> MapLeaguepediaDataToEntity(IEnumerable<JObject> leaguepediaData);
    }
}
