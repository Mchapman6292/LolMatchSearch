using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamRenameRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamRenameToHistoryMappers;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_TeamNameHistoryEntities;

namespace LolMatchFilterNew.Infrastructure.DataConversion.TeamRenameToHistoryMappers
{
    public class TeamRenameToHistoryMapper : ITeamRenameToHistoryMapper
    {
        private readonly IAppLogger _appLogger;
        private readonly ITeamRenameRepository _teamRenameRepository;
        private readonly IGenericRepository<Processed_TeamNameHistoryEntity> _teamHistoryGenericRepository;



        public TeamRenameToHistoryMapper(IAppLogger appLogger, ITeamRenameRepository teamRenameRepository, IGenericRepository<Processed_TeamNameHistoryEntity> teamHistoryGenericRepository)
        {
            _appLogger = appLogger;
            _teamRenameRepository = teamRenameRepository;
            _teamHistoryGenericRepository = teamHistoryGenericRepository;
        }

        public async Task<List<Processed_TeamNameHistoryEntity>> MapTeamRenameToHistoryAsync()
        {
            try
            {
                var teamHistories = await _teamRenameRepository.AddOriginalNameToNewNameAsync();

                var historyEntities = teamHistories.Select(kvp => new Processed_TeamNameHistoryEntity
                {
                    CurrentTeamName = kvp.Key,

                    // Stores previous TeamNames as comma separated string in database. 
                    NameHistory = string.Join(";", kvp.Value) ?? string.Empty
                }).ToList();

                _appLogger.Info($"Mapped {historyEntities.Count} team histories successfully");
                return historyEntities;
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error mapping team rename history: {ex.Message}", ex);
                throw;
            }
        }



    }
}
