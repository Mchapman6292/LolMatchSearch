using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.IMatchServiceControllers;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.DTOs.MatchComparisonResultDTOs;

namespace LolMatchFilterNew.Application.MatchPairingService.MatchServiceControllers
{
    public class MatchServiceController : IMatchServiceController
    {
        private readonly IAppLogger _appLogger;



        public MatchServiceController(IAppLogger appLogger) 
        {
            _appLogger = appLogger;
        }

    }
}
