using Domain.DTOs.InfrastructureDTO.TeamsInLeagueDTOS;

namespace Domain.Interfaces.InfrastructureInterfaces.Repositories.MultipleTableRepositories.ICrossTableRepositories
{
    public interface ICrossTableRepository
    {
        Task<List<TeamsInLeagueDTO>> GetTeamLeagueHistoryAsync(IEnumerable<string> targetLeagues);
    }
}
