using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LolMatchFilterNew.Domain.Interfaces.ILeaguepediaApis
{
    public interface ILeaguepediaApi
    {
        Task GetAllGamesForSeason(string tournament, string split, int year);
    }
}
