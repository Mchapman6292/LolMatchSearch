using Domain.DTOs.Western_MatchDTOs;
using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;

namespace Domain.Interfaces.InfrastructureInterfaces.IStoredSqlFunctionCallers
{
    public interface IStoredSqlFunctionCaller
    {
        Task<List<WesternMatchDTO>> GetWesternMatches();

        Task<List<Import_YoutubeDataEntity>> GetYoutubeDataEntitiesForWesternTeamsAsync();

    }
}
