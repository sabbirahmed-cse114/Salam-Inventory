using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;

namespace Salam.Inventory.Domain.RepositoryContracts
{
    public interface IWarehouseRepository : IRepositoryBase<Warehouse, Guid>
    {
        public bool IsTitleDuplicateOrNot(string title, Guid? id = null);

        Task<(IList<Warehouse> data, int total, int totalDisplay)> GetPagedWarehousesAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);

        Task<IList<Warehouse>> GetWarehousesWithProductWarehouseAsync();
        Task<IList<Warehouse>> GetOrderedWarehouseListAsync();
    }
}
