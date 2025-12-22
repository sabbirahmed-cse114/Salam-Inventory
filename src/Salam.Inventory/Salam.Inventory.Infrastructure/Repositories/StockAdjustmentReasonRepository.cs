using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain.RepositoryContracts;

namespace Salam.Inventory.Infrastructure.Repositories
{
    public class StockAdjustmentReasonRepository : Repository<StockAdjustmentReason, Guid>, IStockAdjustmentReasonRepository
    {
        public StockAdjustmentReasonRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
