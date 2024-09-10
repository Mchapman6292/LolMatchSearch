using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LolMatchFilterNew.Domain.ILeaguepediaApis
{
    public interface ILeaguepediaApi
    {
        Task GetCapsAhriMatchesAsync(Activity activity);
    }
}
