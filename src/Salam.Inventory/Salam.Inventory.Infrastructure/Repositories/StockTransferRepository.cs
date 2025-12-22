using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain.RepositoryContracts;
using Salam.Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Salam.Inventory.Infrastructure.Repositories
{
    public class StockTransferRepository : Repository<StockTransfer, Guid>, IStockTransferRepository
    {
        public StockTransferRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<(IList<StockTransfer> data, int total, int totalDisplay)> GetPagedStockTransfersAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await GetDynamicAsync(null, order, 
                x => x.Include(y => y.FromWarehouse)
                      .Include(z => z.ToWarehouse), pageIndex, pageSize, true);
        }

        public async Task<StockTransfer> GetStockTransferWithProductsAsync(Guid id)
        {
            return await GetByIdAsync(x => x.Id.Equals(id), 
                                            x => x.Include(
                                                    y => y.StockTransferProducts)
                                                            .ThenInclude(i => i.Product)
                                                            .ThenInclude(m => m.MeasurementUnit));
        }
    }
}