using System.Linq.Expressions;
using System.Xml.Linq;

namespace Application.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Get entity using a search predicate
        /// </summary>
        /// <param name="predicate">The lamda predicate to filter with</param>
        /// <returns></returns>
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Get all entities with expression
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Get all entities with expression
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Adds an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(TEntity entity);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Adds a collection of entities to the context
        /// </summary>
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes an entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(int id);
        Task DeleteByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Count the number of items matching the filter predicate
        /// </summary>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Checks wether any entity in the database matching the predicate exists.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task Update(TEntity entity);
    }
}
