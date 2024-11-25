using LolMatchFilterNew.Domain.Entities.Processed_TeamNameHistoryEntities;
using LolMatchFilterNew.Domain.Entities.Import_TeamRenameEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.ITeamHistoryLogic
{
    public interface ITeamHistoryLogic
    {
        List<string> FindPreviousTeamNames(string currentName, IEnumerable<Import_TeamRenameEntity> allRenames, Dictionary<string, List<string>> resultsWithMorethanOneOriginalName);
        void LogMultipleMatches(Dictionary<string, List<string>> resultsWithMorethanOneOriginalName);
        Task<List<Processed_TeamNameHistoryEntity>> GetAllPreviousTeamNamesForCurrentTeamName(List<string> currentNames);

    }
}