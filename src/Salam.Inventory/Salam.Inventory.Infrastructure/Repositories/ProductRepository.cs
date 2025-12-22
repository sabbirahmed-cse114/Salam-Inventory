using Salam.Inventory.Domain;
using Microsoft.EntityFrameworkCore;
using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain.RepositoryContracts;
using Salam.Inventory.Domain.Dtos;

namespace Salam.Inventory.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product, Guid>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public bool IsProductNameDuplicateOrNot(string title, Guid? id = null)
        {
            if (id.HasValue)
            {
                return GetCount(x => !x.Id.Equals(id.Value) && x.ProductName.Equals(title)) > 0;
            }
            else
            {
                return GetCount(x => x.ProductName.Equals(title)) > 0;
            }
        }

        public async Task<(IList<Product> data, int total, int totalDisplay)> GetPagedProductListAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            var searchText = search.Value;
            if (string.IsNullOrWhiteSpace(searchText))
                return await GetDynamicAsync(null, order,
                                        x => x
                                        .Include(t => t.Tax)
                                        .Include(c => c.Category)
                                        .Include(m => m.MeasurementUnit)
                                        .Include(iw => iw.ProductWarehouses),
                                        pageIndex, pageSize, true);

            return await GetDynamicAsync(x => x.ProductName.Contains(searchText)
                                         , order,
                                         x => x
                                         .Include(t => t.Tax)
                                         .Include(c => c.Category)
                                         .Include(m => m.MeasurementUnit)
                                         .Include(iw => iw.ProductWarehouses),
                                         pageIndex, pageSize, true);
        }

        public async Task<(IList<ProductListDto> data, int total, int totalDisplay)> GetPagedProductListAsync(
            int pageIndex, int pageSize, ProductSearchDto search, string? order)
        {
            var procedureName = "GetProductList";

            var result = await SqlUtility.QueryWithStoredProcedure_Async<ProductListDto>(
                procedureName,
                new Dictionary<string, object>
                {
                    { "PageIndex", pageIndex },
                    { "PageSize", pageSize },
                    { "OrderBy", order },
                    { "ProductName", string.IsNullOrEmpty(search.ProductName) ?
                        null : search.ProductName},
                    { "Barcode", string.IsNullOrEmpty(search.Barcode) ?
                        null : search.Barcode},
                    { "CategoryId", string.IsNullOrEmpty(search.CategoryId) ?
                        null: Guid.Parse(search.CategoryId)},
                    { "WarehouseId", string.IsNullOrEmpty(search.WarehouseId) ?
                        null: Guid.Parse(search.WarehouseId)},
                    { "PriceFrom", search.PriceFrom.HasValue ? search.PriceFrom : null},
                    { "PriceTo", search.PriceTo.HasValue ? search.PriceTo : null},
                    { "IsActive", search.IsActive == null ? null : search.IsActive },
                    { "BelowMinimumQuantityOfStock", search.BelowMinimumQuantityOfStock }
                },
                new Dictionary<string, Type>
                {
                    { "Total", typeof(int) },
                    { "TotalDisplay", typeof(int) }
                });

            return (result.result, (int)result.outValues["Total"], (int)result.outValues["TotalDisplay"]);
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await GetByIdAsync(x => x.Id.Equals(id), 
                                             y =>
                                             y.Include(t => t.Tax)
                                             .Include(c => c.Category)
                                             .Include(m => m.MeasurementUnit));
        }

        public async Task RemoveMultipleAsync(List<Guid> selectedProductIds)
        {
            var entities = await GetByIdsAsync<Product>(selectedProductIds);
            await RemoveRange_Async(entities);
        }

        public async Task<IList<Product>> GetFilteredProductsAsync(string searchProduct, Guid warehouseId)
        {
            return await GetAsync(x => x.ProductName.Contains(searchProduct), null,
                                    x => x.Include(y => y.ProductWarehouses
                                                       .Where(z => z.WarehouseId
                                                        .Equals(warehouseId))), true);
        }
    }
}