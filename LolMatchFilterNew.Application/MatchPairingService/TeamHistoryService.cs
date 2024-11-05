using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Application.MatchPairingService.TeamHistoryServices
{
    public class TeamHistoryService
    {
        private readonly IAppLogger _appLogger;


        public TeamHistoryService(IAppLogger appLogger)
        {
            _appLogger = appLogger;
        }







    }
}
