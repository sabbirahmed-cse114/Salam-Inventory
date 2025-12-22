using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;

namespace Salam.Inventory.Application.Services
{
    public interface ICategoryManagementService
    {
        Task CreateProductCategoryAsync(ProductCategory category);
        Task DeleteProductCategoryAsync(Guid id);
        Task<(IList<ProductCategory> data, int total, int totalDisplay)> GetProductCategoriesAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task<ProductCategory> GetProductCategoryAsync(Guid id);
        Task UpdateProductCategoryAsync(ProductCategory category);

        Task<IList<ProductCategory>> GetProductCategoriesAsync();
    }
}