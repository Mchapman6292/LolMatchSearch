using Domain.DTOs.Western_MatchDTOs;

namespace Domain.Interfaces.InfrastructureInterfaces.ITestFunctions
{
    public interface ITestFunction
    {
        Task<IEnumerable<WesternMatchDTO>> GetWesternMatches();
    }
}
