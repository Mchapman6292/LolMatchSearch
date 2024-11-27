using LolMatchFilterNew.Domain.Entities.Processed_Entities.Processed_TeamNameHistoryEntities;


namespace LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.ITeamHistoryRepositories
{
    public interface ITeamHistoryRepository
    {
        public interface ITeamHistoryRepository
        {

            Task<Processed_TeamNameHistoryEntity> GetByCurrentTeamNameAsync(string teamName);
            Task<IEnumerable<Processed_TeamNameHistoryEntity>> GetAllTeamHistoriesAsync();
            Task<bool> AddTeamHistoryAsync(Processed_TeamNameHistoryEntity teamHistory);
            Task<bool> UpdateTeamHistoryAsync(Processed_TeamNameHistoryEntity teamHistory);
            Task<bool> DeleteTeamHistoryAsync(string teamName);

            Task<IEnumerable<Processed_TeamNameHistoryEntity>> GetTeamsByRegionAsync(string region);
            Task<bool> TeamExistsAsync(string teamName);
            Task<IEnumerable<string>> GetAllPreviousNamesAsync(string currentTeamName);
            Task<Processed_TeamNameHistoryEntity> GetByPreviousNameAsync(string previousName);
            Task<bool> UpdateTeamNameHistoryAsync(string currentTeamName, string newHistoricalName);
            Task<IEnumerable<Processed_TeamNameHistoryEntity>> GetTeamsWithNameChangeHistoryAsync();
            Task<Dictionary<string, List<string>>> GetAllTeamNameMappingsAsync();
        }
    }
}
