using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain.RepositoryContracts;
using Salam.Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Salam.Inventory.Infrastructure.Repositories
{
    public class StockAdjustmentRepository : Repository<StockAdjustment, Guid>, IStockAdjustmentRepository
    {
        public StockAdjustmentRepository(ApplicationDbContext context) : base(context) { }

        public async Task<(IList<StockAdjustment> data, int total, int totalDisplay)> GetPagedStockAdjustmentList_Async(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            var searchValue = search.Value;
            if(searchValue != null)
            {
                return await GetDynamicAsync(
                    x=> x.Warehouse.WarehouseName.Contains(searchValue) ||
                    x.ReasonOfStockAdjustment.ReasonName.Contains(searchValue),
                    order, x => x.Include(sti => sti.StockAdjustmentProducts)
                                .ThenInclude(i => i.Product)
                                .Include(w => w.Warehouse)
                                .Include(r => r.ReasonOfStockAdjustment)
                                , pageIndex, pageSize, true);
            }
            else
            {
                return await GetDynamicAsync(null, order,
                                x => x
                                .Include(sti => sti.StockAdjustmentProducts)
                                .ThenInclude(i => i.Product)
                                .Include(w => w.Warehouse)
                                .Include(r => r.ReasonOfStockAdjustment)
                                , pageIndex, pageSize, true);
            }
           
        }

        public async Task<StockAdjustment> GetStockAdjustmentByIdWithProductsAsync(Guid id)
        {
            return await GetByIdAsync(x => x.Id.Equals(id),
                                        x => x.Include(y => y.StockAdjustmentProducts));
        }
    }
}