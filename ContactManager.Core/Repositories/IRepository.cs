using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManager.Core.Entities;

namespace ContactManager.Core.Repositories
{
    public interface IRepository<TEntity, in TId> where TEntity: IAggregateRoot<TId>
    {
        IQueryable<TEntity> Entities { get; }
        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);
        void DeleteMany(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        Task<TEntity> FindByIdAsync(TId id);
        Task SaveChangesAsync();
    }
}
