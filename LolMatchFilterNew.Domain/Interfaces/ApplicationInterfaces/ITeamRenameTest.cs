using LolMatchFilterNew.Domain.Entities.TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Entities.TeamRenamesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.ITeamRenameTests
{
    public interface ITeamRenameTests
    {
        void SingleTeamHistory_ShouldMatchExpected();
        void TeamHistory_MultipleTeams_ShouldMatchExpected(string teamName, string expectedHistory);
        void TeamWithNoHistory_ShouldReturnNull();
        List<TeamNameHistoryEntity> GetTestTeamHistory();
        string BuildTeamHistory(string currentName, List<TeamRenameEntity> renameData);
    }
}
