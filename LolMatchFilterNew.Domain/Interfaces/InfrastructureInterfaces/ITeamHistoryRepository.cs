using LolMatchFilterNew.Domain.Entities.TeamNameHistoryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamHistoryRepositories
{
    public interface ITeamHistoryRepository
    {
        public interface ITeamHistoryRepository
        {

            Task<TeamNameHistoryEntity> GetByCurrentTeamNameAsync(string teamName);
            Task<IEnumerable<TeamNameHistoryEntity>> GetAllTeamHistoriesAsync();
            Task<bool> AddTeamHistoryAsync(TeamNameHistoryEntity teamHistory);
            Task<bool> UpdateTeamHistoryAsync(TeamNameHistoryEntity teamHistory);
            Task<bool> DeleteTeamHistoryAsync(string teamName);

            Task<IEnumerable<TeamNameHistoryEntity>> GetTeamsByRegionAsync(string region);
            Task<bool> TeamExistsAsync(string teamName);
            Task<IEnumerable<string>> GetAllPreviousNamesAsync(string currentTeamName);
            Task<TeamNameHistoryEntity> GetByPreviousNameAsync(string previousName);
            Task<bool> UpdateTeamNameHistoryAsync(string currentTeamName, string newHistoricalName);
            Task<IEnumerable<TeamNameHistoryEntity>> GetTeamsWithNameChangeHistoryAsync();
            Task<Dictionary<string, List<string>>> GetAllTeamNameMappingsAsync();
        }
    }
}
