using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.DomainInterfaces.ILeaguepediaQueryService
{
    public interface ILeaguepediaQueryService
    {
        string BuildQueryStringForPlayersChampsInSeason(string tournamentName, int limit = 480, int offset = 0);
    }
}
