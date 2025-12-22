using Salam.Inventory.Domain.Dtos;
using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;
using Salam.Inventory.Domain.RepositoryContracts;

namespace Salam.Inventory.Domain.RepositoryContracts
{
    public interface IProductRepository : IRepositoryBase<Product, Guid>
    {
        bool IsProductNameDuplicateOrNot(string title, Guid? id = null);
        //Task<(IList<Product> data, int total, int totalDisplay)> GetPagedProductListAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task<(IList<ProductListDto> data, int total, int totalDisplay)> GetPagedProductListAsync(
         int pageIndex, int pageSize, ProductSearchDto search, string? order);
        Task<Product> GetProductByIdAsync(Guid id);
        Task RemoveMultipleAsync(List<Guid> selectedProductIds);
        Task<IList<Product>> GetFilteredProductsAsync(string searchProduct, Guid warehouseId);
    }
}