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
            var entity = new LeaguepediaMatchDetailEntity
            {
                LeaguepediaGameIdAndTitle = jsonObject["GameId"]?.ToString(),
                CustomMatchId = GenerateCustomMatchId(jsonObject),
                DateTimeUTC = ParseDateTime(jsonObject["DateTime UTC"]?.ToString()),
                Tournament = jsonObject["Tournament"]?.ToString(),
                Team1 = jsonObject["Team1"]?.ToString(),
                Team2 = jsonObject["Team2"]?.ToString(),
                Team1Picks = ParsePicks(jsonObject["Team1Picks"]?.ToString()),
                Team2Picks = ParsePicks(jsonObject["Team2Picks"]?.ToString()),
                Team1Side = DetermineTeam1Side(jsonObject["Team1Picks"]?.ToString()),
                Winner = DetermineWinner(jsonObject)
            };

            return entity;
        }



    }
}
