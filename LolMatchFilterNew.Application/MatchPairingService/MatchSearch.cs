using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaMatchDetailRepository;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IYoutubeVideoRepository;

namespace LolMatchFilterNew.Application.MatchPairingService
{
    public class MatchSearch
    {
        private readonly IAppLogger _appLogger;




        public MatchSearch(IAppLogger appLogger)
        {
            _appLogger = appLogger;
        }


        public async Task<Dictionary<string, string>> MatchHighlightsToData()
        {

        }
    }
}
