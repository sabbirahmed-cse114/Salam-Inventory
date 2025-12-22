using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;

namespace Salam.Inventory.Application.Services
{
    public interface IStockTransferManagementService
    {
        Task CreateStockTransfer_Async(StockTransfer stockTransfer, List<StockTransferProduct> stockTransferProducts);
        Task DeleteStockTransfer_Async(Guid id);
        Task<StockTransfer> GetStockTransferProducts_Async(Guid id);
        Task<(IList<StockTransfer> data, int total, int totalDisplay)> GetStockTransfers_Async(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}