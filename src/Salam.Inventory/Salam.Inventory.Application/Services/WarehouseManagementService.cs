using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;

namespace Salam.Inventory.Application.Services
{
    public class WarehouseManagementService : IWarehouseManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        public WarehouseManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task CreateWarehouse_Async(Warehouse warehouse)
        {
            var isWarehouseNameDuplicate = _inventoryUnitOfWork.WarehouseRepository.IsWarehouseDuplicateOrNot(warehouse.WarehouseName);

            if (!isWarehouseNameDuplicate)
            { 
                await _inventoryUnitOfWork.WarehouseRepository.AddAsync(warehouse);
                await _inventoryUnitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("Warehouse name is duplicate...");
            }
        }


        public async Task UpdateWarehouse_Async(Warehouse warehouse)
        {
            var isWarehouseNameDuplicate = _inventoryUnitOfWork.WarehouseRepository.IsWarehouseDuplicateOrNot(warehouse.WarehouseName, warehouse.Id);
            if (!isWarehouseNameDuplicate)
            {
                await _inventoryUnitOfWork.WarehouseRepository.EditAsync(warehouse);
                await _inventoryUnitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("Warehouse name is duplicate...");
            }
        }
        public async Task<Warehouse> GetWarehouse_Async(Guid id)
        {
            return await _inventoryUnitOfWork.WarehouseRepository.GetByIdAsync(id);
        }

        public async Task<IList<Warehouse>> GetWarehouseList_Async()
        {
            return await _inventoryUnitOfWork
                        .WarehouseRepository
                        .GetWarehousesWithProductWarehouseAsync();
        }

        public async Task<(IList<Warehouse> data, int total, int totalDisplay)> GetWarehouses_Async(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _inventoryUnitOfWork.WarehouseRepository.GetPagedWarehousesAsync(pageIndex, pageSize, search, order);
        }

        public async Task<IList<Warehouse>> GetOrderedWarehouses_Async()
        {
            return await _inventoryUnitOfWork.WarehouseRepository.GetOrderedWarehouseListAsync();
        }

        public async Task DeleteWarehouse_Async(Guid id)
        {
            await _inventoryUnitOfWork.WarehouseRepository.RemoveAsync(id);
            await _inventoryUnitOfWork.SaveAsync();
        }
    }
}