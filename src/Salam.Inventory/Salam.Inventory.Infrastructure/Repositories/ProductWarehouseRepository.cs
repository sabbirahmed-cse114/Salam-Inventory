using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace Salam.Inventory.Infrastructure.Repositories
{
    public class ProductWarehouseRepository : CompositRepository<ProductWarehouse, Guid>, IProductWarehouseRepository
    {
        public ProductWarehouseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
