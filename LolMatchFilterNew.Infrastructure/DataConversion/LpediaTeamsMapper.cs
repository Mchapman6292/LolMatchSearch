using LolMatchFilterNew.Domain.Entities.LpediaTeamEntities;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Infrastructure.DataConversion.LpediaTeamsMapper
{
    public class LpediaTeamsMapper
    {
        private readonly IAppLogger _appLogger;
        private readonly IApiHelper _apiHelper;


        public LpediaTeamsMapper(IAppLogger appLogger, IApiHelper apiHelper)
        {
            _appLogger = appLogger;
            _apiHelper = apiHelper;
        }

      


    }
}
