using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;

namespace Salam.Inventory.Application.Services
{
    public class CategoryManagementService : ICategoryManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        public CategoryManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task<(IList<ProductCategory> data, int total, int totalDisplay)> GetProductCategoriesAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _inventoryUnitOfWork.CategoryRepository.GetPagedCategoriesAsync(
                pageIndex, pageSize, search, order);
        }

        public async Task CreateProductCategoryAsync(ProductCategory category)
        {
            var duplicateCategoryName = _inventoryUnitOfWork.CategoryRepository.IsCategoryNameDuplicateOrNot(category.Name);

            if (!duplicateCategoryName)
            {
                await _inventoryUnitOfWork.CategoryRepository.AddAsync(category);
                await _inventoryUnitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("Category name is duplicate...");
            }
        }

        public async Task<ProductCategory> GetProductCategoryAsync(Guid id)
        {
            return await _inventoryUnitOfWork.CategoryRepository.GetByIdAsync(id);
        }

        public async Task UpdateProductCategoryAsync(ProductCategory category)
        {
            if (!_inventoryUnitOfWork.CategoryRepository.IsCategoryNameDuplicateOrNot(category.Name, category.Id))
            {
                await _inventoryUnitOfWork.CategoryRepository.EditAsync(category);
                await _inventoryUnitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("Category name is duplicate...");
            }
        }

        public async Task DeleteProductCategoryAsync(Guid id)
        {
            await _inventoryUnitOfWork.CategoryRepository.RemoveAsync(id);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task<IList<ProductCategory>> GetProductCategoriesAsync()
        {
            return await _inventoryUnitOfWork.CategoryRepository.GetOrderedProductCategoriesAsync();
        }
    }
}