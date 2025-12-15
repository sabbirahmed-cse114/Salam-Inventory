using Salam.Inventory.Domain.Entities;
using System.Linq.Expressions;

namespace Salam.Inventory.Domain.RepositoryContracts
{
    public interface IRepositoryBase<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IComparable
    {
        void Add(TEntity entity);
        void Edit(TEntity entityToUpdate);
        IList<TEntity> GetAll();
        TEntity GetById(TKey id);
        int GetCount(Expression<Func<TEntity, bool>> filter = null);
        void Remove(Expression<Func<TEntity, bool>> filter);
        void Remove(TEntity entityToDelete);
        void Remove(TKey id);
        Task AddAsync(TEntity entity);
        Task AddAsync(List<TEntity> entities);        
        Task EditAsync(TEntity entityToUpdate);        
        Task<IList<TEntity>> GetAllAsync();        
        Task<TEntity> GetByIdAsync(TKey id);        
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null);        
        Task RemoveAsync(Expression<Func<TEntity, bool>> filter);
        Task RemoveAsync(TEntity entityToDelete);
        Task RemoveRangeAsync(IList<TEntity> entitiesToDelete);
        Task RemoveAsync(TKey id);
    }
}