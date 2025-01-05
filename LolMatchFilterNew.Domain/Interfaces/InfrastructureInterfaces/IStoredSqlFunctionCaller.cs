using Domain.DTOs.Western_MatchDTOs;

namespace Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers
{
    public interface IStoredSqlFunctionCaller
    {
        Task<List<WesternMatchDTO>> GetWesternMatches();

    }
}
