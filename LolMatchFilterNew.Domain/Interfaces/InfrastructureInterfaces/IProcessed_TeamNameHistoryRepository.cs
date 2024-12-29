

namespace LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_TeamRenameRepositories
{
    public interface IProcessed_TeamNameHistoryRepository
    {
        Task<List<string>> TESTGetTeamsByNameAsync(string teamName);
    }
}
