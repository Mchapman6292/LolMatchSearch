using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using Newtonsoft.Json.Linq;

namespace LolMatchFilterNew.Infrastructure.DataConversion
{
    public class LeaguepediaEntityMapper
    {
        private readonly IAppLogger _appLogger;


        public LeaguepediaEntityMapper(IAppLogger appLogger)
        {
            _appLogger = appLogger;
        }


        private LeaguepediaMatchDetailEntity MapToEntity(JObject jsonObject)
        {
            throw new NotImplementedException();
        }



    }
}
