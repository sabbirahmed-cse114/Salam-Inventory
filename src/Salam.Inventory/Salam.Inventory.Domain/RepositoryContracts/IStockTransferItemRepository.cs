using Salam.Inventory.Domain.Entities;

namespace Salam.Inventory.Domain.RepositoryContracts
{
    public interface IStockTransferItemRepository : IRepositoryBase<StockTransferProduct, Guid>
    {
    }
}