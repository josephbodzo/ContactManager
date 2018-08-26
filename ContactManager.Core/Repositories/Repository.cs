using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManager.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Core.Repositories
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, IAggregateRoot<TId>
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly DbContext _dbContext;

        public Repository( DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Entities => _dbSet;

        public async Task AddAsync(TEntity entity)
        {
           await _dbSet.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public void DeleteMany(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<TEntity> FindByIdAsync(TId id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
