using Domain.DTOs.TeamnameDTOs;
using Domain.DTOs.Western_MatchDTOs;

namespace Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers
{
    public interface IStoredSqlFunctionCaller
    {
        Task<IEnumerable<WesternMatchDTO>> GetWesternMatches();

        Task<IEnumerable<TeamnameDTO>> GetWesternTeamsWithRegions();
    }
}
