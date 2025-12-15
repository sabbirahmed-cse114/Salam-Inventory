using Salam.Inventory.Domain.Entities;

namespace Salam.Inventory.Domain.RepositoryContracts
{
    public interface ICompositRepositoryBase<ICompositEntity, TKey>
        where ICompositEntity : class, ICompositEntity<TKey> 
        where TKey : IComparable
    {
        Task AddAsync(ICompositEntity entity);
        Task AddAsync(List<ICompositEntity> entities);
        Task<ICompositEntity> GetByIdAsync(TKey id1, TKey id2);
        Task<IEnumerable<ICompositEntity>> GetAllAsync();        
        Task EditAsync(ICompositEntity entity);
        Task RemoveAsync(TKey id1, TKey id2);
    }
}