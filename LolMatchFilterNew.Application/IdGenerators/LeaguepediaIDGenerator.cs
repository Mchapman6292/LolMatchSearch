using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.ILeaguepediaIDGenerators;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Application.IdGenerators.LeaguepediaIDGenerators
{
    public class LeaguepediaIDGenerator : ILeaguepediaIDGenerator
    {
        private readonly IAppLogger _appLogger;


        public LeaguepediaIDGenerator(IAppLogger appLogger)
        {
            _appLogger = appLogger;
        }


        
    }
}
