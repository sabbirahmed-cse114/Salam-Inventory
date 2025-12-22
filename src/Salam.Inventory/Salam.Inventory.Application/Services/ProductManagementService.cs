using Salam.Inventory.Domain.Dtos;
using Salam.Inventory.Domain.Entities;

namespace Salam.Inventory.Application.Services
{
    public class ProductManagementService : IProductManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        public ProductManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task CreateProduct_Async(Product product)
        {
            var isProductDuplicate = _inventoryUnitOfWork.ProductRepository.IsProductNameDuplicateOrNot(product.ProductName);

            if (!isProductDuplicate)
            {
                await _inventoryUnitOfWork.ProductRepository.AddAsync(product);
                await _inventoryUnitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("Name is duplicate...");
            }
        }

        public async Task CreateProductWarehouse_Async(List<ProductWarehouse> productWarehouses)
        {
            await _inventoryUnitOfWork.ProductWarehouseRepository.AddAsync(productWarehouses);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task DeleteProduct_Async(Guid id)
        {
            await _inventoryUnitOfWork.ProductRepository.RemoveAsync(id);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task DeleteMultipleProducts_Async(List<Guid>? selectedProductIds)
        {
            if (selectedProductIds != null && selectedProductIds.Count > 0)
            {
                await _inventoryUnitOfWork.ProductRepository.RemoveMultipleAsync(selectedProductIds);
                await _inventoryUnitOfWork.SaveAsync();
            }
        }

        public async Task<Product> GetProduct_Async(Guid id)
        {
            return await _inventoryUnitOfWork.ProductRepository.GetProductByIdAsync(id);
        }

        public async Task<(IList<ProductListDto> data, int total, int totalDisplay)> GetProducts_Async(int pageIndex, int pageSize, ProductSearchDto search, string? order)
        {
            return await _inventoryUnitOfWork.ProductRepository.GetPagedProductListAsync(pageIndex, pageSize, search, order);
        }

        public async Task UpdateProduct_Async(Product product)
        {
            var isProductNameDuplicate = _inventoryUnitOfWork.ProductRepository.IsProductNameDuplicateOrNot(product.ProductName, product.Id);

            if (!isProductNameDuplicate)
            {
                await _inventoryUnitOfWork.ProductRepository.EditAsync(product);
                await _inventoryUnitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("Name is duplicate...");
            }
        }

        public async Task<IList<Product>> GetProductsForStockTransfer_Async(string searchProduct, Guid warehouseId)
        {
            return await _inventoryUnitOfWork.ProductRepository.GetFilteredProductsAsync(searchProduct, warehouseId);
        }

        public async Task<double> GetProductsCount_Async()
        {
            return await _inventoryUnitOfWork.ProductRepository.GetCountAsync();
        }
    }
}