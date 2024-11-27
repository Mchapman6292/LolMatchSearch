using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_ScoreboardGamesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ILeaguepediaMatchDetailRepository
{
    public interface ILeaguepediaMatchDetailRepository
    {
        Task<int> BulkAddScoreboardGames(IEnumerable<Import_ScoreboardGamesEntity> matchDetails);
        Task<int> DeleteAllScoreboardGames();
    }
}
