using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using Microsoft.EntityFrameworkCore;


namespace LolMatchFilterNew.infrastructure.Repositories.GenericRepositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly DbSet<T> _dbSet;
        private readonly DbContext _context;
        private readonly IAppLogger _appLogger;

        public GenericRepository(DbContext context, IAppLogger appLogger)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _appLogger = appLogger;
        }

        // Virtual allows for a method to be overridden in derived classes. 
        //Update/RemoveEntity mark the entities as modified or deleted in the change tracker.The actual database operations occur when SaveChanges() or SaveChangesAsync() are called.
        public virtual async Task<T> GetIdAsync(object id)
        {
            if (id == null)
            {
                _appLogger.Error($"ERROR null parameter for {nameof(GetIdAsync)}.");
                throw new ArgumentNullException(nameof(id), "The id parameter cannot be null");
            }
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetMultipleIdsAsync(params object[] ids)
        {
            var results = new List<T>();

            foreach (var id in ids)
            {
                var searchResult = await _dbSet.FindAsync(id);
                if (searchResult != null)
                {
                    results.Add(searchResult);
                }
            }
            return results;
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public virtual void RemoveEntity(T entity)
        {
            _dbSet.Remove(entity);
        }


        public virtual void RemoveEntities(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}