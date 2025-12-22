using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain.RepositoryContracts;

namespace Salam.Inventory.Infrastructure.Repositories
{
    public class StockTransferProductRepository : Repository<StockTransferProduct, Guid>, IStockTransferProductRepository
    {
        public StockTransferProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
