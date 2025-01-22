using Domain.DTOs.Western_MatchDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_ScoreboardGamesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_ScoreboardGamesRepositories
{
    public interface IImport_ScoreboardGamesRepository
    {
        Task<int> BulkAddScoreboardGames(IEnumerable<Import_ScoreboardGamesEntity> matchDetails);

        Task<List<WesternMatchDTO>> GetEuNaMatchesWithinDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
