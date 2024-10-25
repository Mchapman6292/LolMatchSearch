using LolMatchFilterNew.Domain.Interfaces.IAppLoggers;
using LolMatchFilterNew.Domain.Interfaces.IGenericRepositories;
using Microsoft.EntityFrameworkCore;


namespace LolMatchFilterNew.Infrastructure.Repositories.GenericRepositories
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
        // Transactions used only for methods that modify data, not read only operations(Get).
        public virtual async Task<T> GetIdAsync(object id)
        {
            if (id == null)
            {
                _appLogger.Error($"Null parameter for {nameof(GetIdAsync)}.");
                throw new ArgumentNullException(nameof(id), "The id parameter cannot be null");
            }
            try
            {
                var result = await _dbSet.FindAsync(id);
                if (result == null)
                {
                    _appLogger.Warning($"No entity found with id {id} in {nameof(GetIdAsync)}.");
                }
                return result;
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error in {nameof(GetIdAsync)}: {ex.Message}");
                throw;
            }
        }


        public virtual async Task<IEnumerable<T>> GetMultipleIdsAsync(params object[] ids)
        {
            var results = new List<T>();
            try
            {
                foreach (var id in ids)
                {
                    var searchResult = await _dbSet.FindAsync(id);
                    if (searchResult != null)
                    {
                        results.Add(searchResult);
                    }
                    else
                    {
                        _appLogger.Warning($"No entity found with id {id} in {nameof(GetMultipleIdsAsync)}.");
                    }
                }
                _appLogger.Info($"Retrieved {results.Count} out of {ids.Length} requested entities in {nameof(GetMultipleIdsAsync)}.");
                return results;
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error in {nameof(GetMultipleIdsAsync)}: {ex.Message}");
                throw;
            }
        }


        public virtual async Task AddAsync(T entity)
        {
            if (entity == null)
            {
                _appLogger.Error($"Null entity parameter in {nameof(AddAsync)}.");
                throw new ArgumentNullException(nameof(entity), "The entity parameter cannot be null");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                _appLogger.Info($"Entity of type {typeof(T).Name} added successfully in {nameof(AddAsync)}.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _appLogger.Error($"Error in {nameof(AddAsync)}: {ex.Message}");
                throw;
            }
        }

        public async Task<(int savedCount, int failedCount)> AddRangeWithTransactionAsync(IEnumerable<T> entities)
        {
            int savedCount = 0;
            int failedCount = 0;
            string entityTypeName = typeof(T).Name;

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var entity in entities)
                {
                    try
                    {
                        await _dbSet.AddAsync(entity);
                        await _context.SaveChangesAsync();
                        savedCount++;
                    }
                    catch (Exception ex)
                    {
                        _appLogger.Error($"Failed to save {entityTypeName} entity: {ex.Message}");
                        failedCount++;
                    }
                }

                if (failedCount > 0)
                {
                    await transaction.RollbackAsync();
                    _appLogger.Warning($"Transaction rolled back. {failedCount} {entityTypeName} entries failed to save.");
                    return (0, entities.Count());
                }

                await transaction.CommitAsync();
                _appLogger.Info($"Successfully added {savedCount} {entityTypeName} entities.");
                return (savedCount, failedCount);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _appLogger.Error($"Transaction rolled back due to error while saving {entityTypeName} entities: {ex.Message}");
                return (0, entities.Count());
            }
        }

        public virtual async Task RemoveEntityAsync(T entity)
        {
            if (entity == null)
            {
                _appLogger.Error($"Null entity parameter in {nameof(RemoveEntityAsync)}.");
                throw new ArgumentNullException(nameof(entity), "The entity parameter cannot be null");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                _appLogger.Info($"Entity of type {typeof(T).Name} removed successfully in {nameof(RemoveEntityAsync)}.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _appLogger.Error($"Error in {nameof(RemoveEntityAsync)}: {ex.Message}");
                throw;
            }
        }


        public virtual async Task RemoveEntitiesAsync(IEnumerable<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                _appLogger.Warning($"No entities to remove in {nameof(RemoveEntitiesAsync)}.");
                return;
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _dbSet.RemoveRange(entities);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                _appLogger.Info($"{entities.Count()} entities of type {typeof(T).Name} removed successfully in {nameof(RemoveEntitiesAsync)}.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _appLogger.Error($"Error in {nameof(RemoveEntitiesAsync)}: {ex.Message}");
                throw;
            }
        }

        public async Task<DateTime> GetEarliestDateTimePublishedAt<TEntity1, TEntity2>()
                where TEntity1 : class
                where TEntity2 : class
        {
            var query1 = _context.Set<TEntity1>();
            var query2 = _context.Set<TEntity2>();

 
            var count1 = await query1.CountAsync();
            var count2 = await query2.CountAsync();
            var lowestCount = Math.Min(count1, count2);
        }

        public async Task<(List<TEntity1>, List<TEntity2>)> GetAllEntitiesAsync<TEntity1, TEntity2>()
                where TEntity1 : class
                where TEntity2 : class
        {
            var entity1List = await _context.Set<TEntity1>().ToListAsync();
            var entity2List = await _context.Set<TEntity2>().ToListAsync();

            return (entity1List, entity2List); 
        }



    }
}