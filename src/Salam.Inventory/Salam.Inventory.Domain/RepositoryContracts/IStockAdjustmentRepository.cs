using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;

namespace Salam.Inventory.Domain.RepositoryContracts
{
    public interface IStockAdjustmentRepository : IRepositoryBase<StockAdjustment, Guid>
    {
        Task<(IList<StockAdjustment> data, int total, int totalDisplay)> GetPagedStockAdjustmentListAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task<StockAdjustment> GetStockAdjustmentByIdWithProductsAsync(Guid id);
    }
}