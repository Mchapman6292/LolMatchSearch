using LolMatchFilterNew.Domain.Entities.TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamRenameRepositories;
using LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamRenameToHistoryMappers;
using LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.ITeamHistoryLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Application.TeamHistoryService.TeamHistoryLogics
{
    public class TeamHistoryLogic : ITeamHistoryLogic
    {
        private readonly IAppLogger _appLogger;
        private readonly ITeamRenameRepository _teamRenameRepository;
        private readonly IGenericRepository<TeamNameHistoryEntity> _teamHistoryEntity;

        public TeamHistoryLogic(IAppLogger appLogger, ITeamRenameRepository teamRenameRepository, IGenericRepository<TeamNameHistoryEntity> teamHistoryEntity)
        {
            _appLogger = appLogger;
            _teamRenameRepository = teamRenameRepository;
            _teamHistoryEntity = teamHistoryEntity;
        }
    }
}
