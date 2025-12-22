using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain.RepositoryContracts;
using Salam.Inventory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Salam.Inventory.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<ProductCategory, Guid>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<(IList<ProductCategory> data, int total, int totalDisplay)> GetPagedCategoriesAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            var searchText = search.Value;
            if (string.IsNullOrWhiteSpace(searchText))
                return await GetDynamicAsync(null, order, null, pageIndex, pageSize, true);

            return await GetDynamicAsync(x => x.Name.Contains(searchText) ||
                                         x.Description.Contains(searchText),
                                         order, null, pageIndex, pageSize, true);
        }

        public bool IsCategoryNameDuplicateOrNot(string title, Guid? id = null)
        {
            if (id.HasValue)
            {
                return GetCount(x => !x.Id.Equals(id.Value) && x.Name.Equals(title)) > 0;
            }
            else
            {
                return GetCount(x => x.Name.Equals(title)) > 0;
            }
        }

        public async Task<IList<ProductCategory>> GetOrderedProductCategoriesAsync()
        {
            return await GetAsync(null, x => x.OrderBy(y => y.Name), null, true);
        }
    }
}