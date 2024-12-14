using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_TeamRenameEntities;
using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_TeamNameHistoryEntities;

namespace LolMatchFilterNew.Domain.Interfaces.ApplicationInterfaces.ITeamHistoryLogic
{
    public interface ITeamHistoryLogic
    {
        List<string>? FindPreviousTeamNames(string currentName, IEnumerable<Import_TeamRenameEntity> allRenames);
        void LogMultipleMatches(Dictionary<string, List<string>> resultsWithMorethanOneOriginalName);
        Task<List<Processed_TeamNameHistoryEntity>> GetAllPreviousTeamNamesForCurrentTeamName(List<string> currentNames);

    }
}