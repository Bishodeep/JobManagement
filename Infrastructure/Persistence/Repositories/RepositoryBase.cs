using Application.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using System.Security.Principal;
using System.Xml.Linq;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected readonly bool _useTracking;


        public RepositoryBase(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        /// <summary>
        /// Add an entity so it gets attached to the context. 
        /// The entity is not saved yet!
        /// </summary>
        public async virtual Task AddAsync(TEntity entity) => await AddAsync(entity, CancellationToken.None);
        public async virtual Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Add a range of entities in 1 call
        /// this will improve performance for larger sets
        /// </summary>
        public async Task AddRangeAsync(IEnumerable<TEntity> entities) => await AddRangeAsync(entities, CancellationToken.None);
        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        /// <summary>
        /// Mark an entity as deleted. 
        /// </summary>
        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Mark an entity as deleted by supplying its Id
        /// </summary>
        public async virtual Task DeleteByIdAsync(int id) => await DeleteByIdAsync(id, CancellationToken.None);
        public async virtual Task DeleteByIdAsync(int id, CancellationToken cancellationToken)
        {
            TEntity? entity = await GetByIdAsync(id, cancellationToken);

            if (entity is null)
            {
                throw new Exception();
            }

            Delete(entity);
        }

        /// <summary>
        /// Retrieve all entities from the database
        /// </summary>
        public virtual IQueryable<TEntity> GetAll()
        {
            return GetAll(null);
        }

        /// <summary>
        /// Count the number of items matching the filter predicate
        /// </summary>
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate) => await CountAsync(predicate, CancellationToken.None);
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.CountAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// Retrieve all entities from the database based on a predicate filter
        /// </summary>
        /// <param name="predicate">Lambda predicate to filter the query on</param>
        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? predicate)
        {
            var entities = _dbSet as IQueryable<TEntity>;

            if (!_useTracking)
            {
                entities = entities.AsNoTracking();
            }

            if (predicate != null)
            {
                entities = entities.Where(predicate);
            }

            return entities;
        }

        /// <summary>
        /// Retrieve an entity from context using its Id
        /// </summary>
        public async virtual Task<TEntity?> GetByIdAsync(int id) => await GetByIdAsync(id, CancellationToken.None);
        public async virtual Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            if (!_useTracking)
            {
                return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            }

            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieve an entity from context using a lambda predicate
        /// </summary>
        public async virtual Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate) => await FindAsync(predicate, CancellationToken.None);
        public async virtual Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            if (!_useTracking)
            {
                return await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
            }

            return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// Check if any entity exists in the database matching the lambda predicate
        /// </summary>
        public async virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate) => await ExistsAsync(predicate, CancellationToken.None);
        public async virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(predicate, cancellationToken);
        }

        
        public async Task Update(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
