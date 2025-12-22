using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain.RepositoryContracts;

namespace Salam.Inventory.Infrastructure.Repositories
{
    public class StockAdjustmentProductRepository : Repository<StockAdjustmentProduct, Guid>, IStockAdjustmentProductRepository
    {
        public StockAdjustmentProductRepository(ApplicationDbContext context) : base(context) { }

    }
}
