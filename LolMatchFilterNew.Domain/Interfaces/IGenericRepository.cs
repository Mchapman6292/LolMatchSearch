using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.IGenericRepositories
{
    public interface IGenericRepository<in T> where T : class
    {

    }
}
