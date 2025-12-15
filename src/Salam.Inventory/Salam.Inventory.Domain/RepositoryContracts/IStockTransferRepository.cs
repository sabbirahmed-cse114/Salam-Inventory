using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;

namespace Salam.Inventory.Domain.RepositoryContracts
{
    public interface IStockTransferRepository : IRepositoryBase<StockTransfer, Guid>
    {
        Task<(IList<StockTransfer> data, int total, int totalDisplay)> GetPagedStockTransfersAsync
            (int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task<StockTransfer> GetStockTransferWithProductsAsync(Guid id);
    }
}
