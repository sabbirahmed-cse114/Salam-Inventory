using Salam.Inventory.Domain.Dtos;
using Salam.Inventory.Domain.Entities;

namespace Salam.Inventory.Application.Services
{
    public interface IProductManagementService
    {
        Task CreateProduct_Async(Product product);
        Task<Product> GetProduct_Async(Guid id);
        Task DeleteProduct_Async(Guid id);
        Task<(IList<ProductListDto> data, int total, int totalDisplay)> GetProducts_Async(int pageNumber, int page_Size, ProductSearchDto search, string? order);
        Task UpdateProduct_Async(Product product);
        Task CreateProductWarehouse_Async(List<ProductWarehouse> warehouses);
        Task DeleteMultipleProducts_Async(List<Guid>? selectedProductIds);
        Task<IList<Product>> GetProductsForStockTransfer_Async(string searchProduct, Guid warehouseId);
        Task<double> GetProductsCount_Async();
    }
}