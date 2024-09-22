namespace LolMatchFilterNew.Domain.Interfaces.IGenericRepositories
{
    // 
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetIdAsync(object id);

        Task<IEnumerable<T>> GetMultipleIdsAsync(params object[] ids);

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        void RemoveEntity(T entity);

        void RemoveEntities(IEnumerable<T> entities);
    }
}

