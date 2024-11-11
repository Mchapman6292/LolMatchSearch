using LolMatchFilterNew.Domain.Entities.TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Entities.TeamRenamesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.ITeamHistoryLogic
{
    public interface ITeamHistoryLogic
    {
        List<string> FindPreviousTeamNames(string currentName, IEnumerable<TeamRenameEntity> allRenames, Dictionary<string, List<string>> resultsWithMorethanOneOriginalName);
        void LogMultipleMatches(Dictionary<string, List<string>> resultsWithMorethanOneOriginalName);
        Task<List<TeamNameHistoryEntity>> GetAllPreviousTeamNamesForCurrentTeamName(List<string> currentNames);

    }
}