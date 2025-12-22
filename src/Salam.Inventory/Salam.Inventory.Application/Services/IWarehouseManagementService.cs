using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;

namespace Salam.Inventory.Application.Services
{
    public interface IWarehouseManagementService
    {
        Task CreateWarehouse_Async(Warehouse warehouse);
        Task UpdateWarehouse_Async(Warehouse warehouse);
        Task<Warehouse> GetWarehouse_Async(Guid id);
        Task<IList<Warehouse>> GetWarehouseList_Async();
        Task<(IList<Warehouse> data, int total, int totalDisplay)> GetWarehouses_Async(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task DeleteWarehouse_Async(Guid id);
        Task<IList<Warehouse>> GetOrderedWarehouses_Async();
    }
}