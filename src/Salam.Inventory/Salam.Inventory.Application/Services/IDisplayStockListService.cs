using Salam.Inventory.Domain.Dtos;
using Salam.Inventory.Domain.Entities;

namespace Salam.Inventory.Application.Services
{
    public interface IDisplayStockListService
    {
        Task<(IList<StockListDto> data, int total, int totalDisplay)> GetStockList_Async(
            int pageIndex, int pageSize, StockListSearchDto search, string? order);
        Task<IList<ProductWarehouse>> GetTotalStockValue_Async();
    }
}