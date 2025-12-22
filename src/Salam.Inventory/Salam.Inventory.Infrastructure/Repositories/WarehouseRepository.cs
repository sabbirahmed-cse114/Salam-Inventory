using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain.RepositoryContracts;
using Salam.Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Salam.Inventory.Infrastructure.Repositories
{
    public class WarehouseRepository : Repository<Warehouse, Guid>, IWarehouseRepository
    {
        public WarehouseRepository(ApplicationDbContext context) : base(context)
        {
        }
        public bool IsWarehouseDuplicateOrNot(string title, Guid? id = null)
        {
            if (id.HasValue)
            {
                return GetCount(x => !x.Id.Equals(id.Value) && x.WarehouseName.Equals(title)) > 0;
            }
            else
            {
                return GetCount(x => x.WarehouseName.Equals(title)) > 0;
            }
        }

        public async Task<(IList<Warehouse> data, int total, int totalDisplay)> GetPagedWarehousesAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            var searchText = search.Value;
            if (string.IsNullOrWhiteSpace(searchText))
                return await GetDynamicAsync(null, order, null, pageIndex, pageSize, true);

            return await GetDynamicAsync(x => x.WarehouseName.Contains(searchText),
                                         order, null, pageIndex, pageSize, true);
        }

        public async Task<IList<Warehouse>> GetWarehousesWithProductWarehouseAsync()
        {
            return await GetAsync(null, x => x.OrderBy(y => y.WarehouseName),
                                        x => x.Include(y => y.ProductWarehouses), true);
        }

        public Task<IList<Warehouse>> GetOrderedWarehouseListAsync()
        {
            return GetAsync(null, x => x.OrderBy(y => y.WarehouseName), null, true);
        }
    }
}