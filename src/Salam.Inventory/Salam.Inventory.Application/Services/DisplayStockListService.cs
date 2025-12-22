using Salam.Inventory.Domain.Dtos;
using Salam.Inventory.Domain.Entities;

namespace Salam.Inventory.Application.Services
{
    public class DisplayStockListService : IDisplayStockListService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        public DisplayStockListService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task<(IList<StockListDto> data, int total, int totalDisplay)> GetStockList_Async(int pageIndex, int pageSize, StockListSearchDto search, string? order)
        {
            return await _inventoryUnitOfWork.GetPagedStockList_Async(pageIndex, pageSize, search, order);
        }

        public async Task<IList<ProductWarehouse>> GetTotalStockValue_Async()
        {
            return (IList<ProductWarehouse>)await _inventoryUnitOfWork
                                            .ProductWarehouseRepository.GetAllAsync();
        }
    }
}