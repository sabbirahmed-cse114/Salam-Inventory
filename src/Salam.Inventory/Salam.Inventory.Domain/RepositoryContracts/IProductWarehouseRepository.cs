using Salam.Inventory.Domain.Entities;

namespace Salam.Inventory.Domain.RepositoryContracts
{
    public interface IProductWarehouseRepository : ICompositRepositoryBase<ProductWarehouse, Guid>
    {
    }
}
