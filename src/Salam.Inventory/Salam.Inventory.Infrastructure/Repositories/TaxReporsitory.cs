using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain.RepositoryContracts;
using Salam.Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Salam.Inventory.Infrastructure.Repositories
{
    public class TaxReporsitory : Repository<Tax, Guid>, ITaxRepository
    {
        public TaxReporsitory(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<(IList<Tax> data, int total, int totalDisplay)> GetAllTaxesAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await GetDynamicAsync(null, order, null, pageIndex, pageSize, true);
        }

        public async Task<IList<Tax>> GetOrderedTaxes()
        {
            return await GetAsync(null, x => x.OrderBy(y => y.Parcentage), null, true);
        }
    }
}