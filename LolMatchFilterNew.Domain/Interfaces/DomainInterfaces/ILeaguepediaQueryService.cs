using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryService
{
    public interface ILeaguepediaQueryService
    {
        string BuildLeaguepediaQuery(string tournamentName, string split, int year, int limit = 5);
    }
}
