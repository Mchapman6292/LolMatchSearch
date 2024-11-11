using LolMatchFilterNew.Domain.Entities.TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Entities.TeamRenamesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.ITeamRenameTests
{
    public interface ITeamRenameTest
    {


        Task AllTeamsHistory_ShouldMatchExpected();
        Task SingleTeamHistory_ShouldMatchExpected(string teamName, string expectedHistory);
    }
}
