namespace LolMatchFilterNew.Domain.Interfaces.IGenericRepositories
{
    // 
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetIdAsync(object id);

        Task<IEnumerable<T>> GetMultipleIdsAsync(params object[] ids);

        Task AddAsync(T entity);

        Task<(int savedCount, int failedCount)> AddRangeWithTransactionAsync(IEnumerable<T> entities);

        Task RemoveEntityAsync(T entity);

        Task RemoveEntitiesAsync(IEnumerable<T> entities);

        Task<IEnumerable<T>> GetAllValuesAsync();

        Task<(List<TEntity1>, List<TEntity2>)> GetAllEntitiesAsync<TEntity1, TEntity2>()
            where TEntity1 : class
            where TEntity2 : class;
    }
}

