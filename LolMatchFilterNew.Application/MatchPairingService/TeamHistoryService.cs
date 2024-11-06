using LolMatchFilterNew.Domain.Entities.TeamRenamesEntities;
using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Infrastructure.Repositories.TeamRenameRepositories;

namespace LolMatchFilterNew.Application.MatchPairingService.TeamHistoryServices
{
    public class TeamHistoryService
    {
        private readonly IAppLogger _appLogger;
        private readonly IGenericRepository<TeamRenameEntity> _TeamRenameGenericRepository;


        public TeamHistoryService(IAppLogger appLogger, IGenericRepository<TeamRenameEntity> teamRenameGenericRepository)
        {
            _appLogger = appLogger;
            _TeamRenameGenericRepository = teamRenameGenericRepository;
        }


        public async Task<List<string>> LinkPreviousTeamNamesToCurrentTeamName()
        {

        }





    }
}
