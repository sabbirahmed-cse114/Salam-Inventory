using Salam.Inventory.Domain.Entities;

namespace Salam.Inventory.Domain.RepositoryContracts
{
    public interface ICategoryRepository : IRepositoryBase<ProductCategory, Guid>
    {
        Task<(IList<ProductCategory> data, int total, int totalDisplay)> GetPagedCategoriesAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        bool IsCategoryNameDuplicateOrNot(string title, Guid? id = null);
        Task<IList<ProductCategory>> GetOrderedProductCategoriesAsync();
    }
}