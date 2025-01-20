using LolMatchFilterNew.Domain.Entities.Imported_Entities.Import_YoutubeDataEntities;

namespace LolMatchFilterNew.Domain.Interfaces.InfrastructureInterfaces.IImport_YoutubeDataRepositories
{
    public interface IImport_YoutubeDataRepository
    {
        Task<int> BulkaddYoutubeDetails(IEnumerable<Import_YoutubeDataEntity> youtubeVideoDetails);

        Task<int> DeleteAllImport_YoutubeData();

        Task<List<Import_YoutubeDataEntity>> GetAllImport_YoutubeData();

        Task<List<Import_YoutubeDataEntity>> GetEuNaVideosByPlaylistAsync();

    }
}
