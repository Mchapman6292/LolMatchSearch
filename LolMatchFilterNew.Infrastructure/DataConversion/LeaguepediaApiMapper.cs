using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Entities.LeaguepediaMatchDetailEntities;
using LolMatchFilterNew.Domain.Helpers.ApiHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaApiMappers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using LolMatchFilterNew.Domain.Interfaces.IApiHelper;
using LolMatchFilterNew.Domain.Entities.LeagueTeamEntities;

namespace LolMatchFilterNew.Infrastructure.DataConversion.LeaguepediaApiMappers
{
    public class LeaguepediaApiMapper : ILeaguepediaApiMapper
    {
        private readonly IAppLogger _appLogger;
        private readonly IApiHelper _apiHelper;


        public LeaguepediaApiMapper(IAppLogger appLogger, IApiHelper apiHelper)
        {
            _appLogger = appLogger;
            _apiHelper = apiHelper;
        }


        public async Task<IEnumerable<LeaguepediaMatchDetailEntity>> MapLeaguepediaDataToEntity(IEnumerable<JObject> leaguepediaData)
        {
            if (leaguepediaData == null || !leaguepediaData.Any())
            {
                _appLogger.Error($"Input data cannot be null or empty for {nameof(MapLeaguepediaDataToEntity)}.");
                throw new ArgumentNullException(nameof(leaguepediaData), "Input data cannot be null or empty.");
            }
            return await Task.Run(() =>
            {
                var results = new List<LeaguepediaMatchDetailEntity>();
                int processedCount = 0;
                foreach (var matchData in leaguepediaData)
                {
                    processedCount++;
                    try
                    {

                        var entity = new LeaguepediaMatchDetailEntity
                        {
                            LeaguepediaGameIdAndTitle = _apiHelper.GetStringValue(matchData, "GameId"),
                            GameName = _apiHelper.GetStringValue(matchData, "Gamename"),
                            League = _apiHelper.GetStringValue(matchData, "League"),
                            DateTimeUTC = _apiHelper.ParseDateTime(matchData, "DateTime UTC"),
                            Tournament = _apiHelper.GetStringValue(matchData, "Tournament"),
                            Team1 = _apiHelper.GetStringValue(matchData, "Team1"),
                            Team2 = _apiHelper.GetStringValue(matchData, "Team2"),
                            Team1Players = string.Join(',',_apiHelper.GetValuesAsList(matchData, "Team1Players")),
                            Team2Players = string.Join(',', _apiHelper.GetValuesAsList(matchData, "Team2Players")),
                            Team1Picks = string.Join(',', _apiHelper.GetValuesAsList(matchData, "Team1Picks")),
                            Team2Picks = string.Join(',', _apiHelper.GetValuesAsList(matchData, "Team2Picks")),
                            WinTeam = _apiHelper.GetStringValue(matchData,"WinTeam"),
                            LossTeam = _apiHelper.GetStringValue(matchData, "LossTeam"),
                            Team1Kills = _apiHelper.GetInt32Value(matchData, "Team1Kills"),
                            Team2Kills = _apiHelper.GetInt32Value(matchData, "Team2Kills")

                        };

                        results.Add(entity);
                    }
                    catch (Exception ex)
                    {
                        _appLogger.Error($"Error processing match data {processedCount}: {ex.Message}");
                        _appLogger.Error($"Raw data: {matchData}");
                    }
                }

                _appLogger.Info($"Deserialized {results.Count} entities out of {processedCount} processed.");
                return results;
            });
        }

        public async Task<TEntity> MapToEntity<TEntity>(JObject leaguepediaData) where TEntity : class
        {
            try
            {
                return CreateEntity<TEntity>(leaguepediaData);
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error mapping to {typeof(TEntity).Name}: {ex.Message}", ex);
                throw;
            }
        }
    }

    public async Task<TEntity> MapToEntities<TEntity>(IEnumerable<JObject> leaguepediaData) where TEntity : class
    {
        try 
        {
            foreach(var result in leaguepediaData)
            {
                CreateEntity<TEntity>(Leag)
            }
        }
    }



    public async Task<TEntity> CreateEntity<TEntity>(JObject leaguepediaData) where TEntity : class
    {
        throw new NotFiniteNumberException();
    }    

}


