using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;

namespace Salam.Inventory.Application.Services
{
    public interface IStockAdjustmentManagementService
    {
        Task CreateStockAdjustment_Async(StockAdjustment stockAdjustment, List<StockAdjustmentProduct> stockAdjustmentProducts);
        Task CreateStockAdjustmentReason_Async(StockAdjustmentReason reason);
        Task DeleteStockAdjustment_Async(Guid id);
        Task<IList<StockAdjustmentReason>> GetStockAdjustmentReasons_Async();
        Task<(IList<StockAdjustment> data, int total, int totalDisplay)> GetStockAdjustments_Async(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}