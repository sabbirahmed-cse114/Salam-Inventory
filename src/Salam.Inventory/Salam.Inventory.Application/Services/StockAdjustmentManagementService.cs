using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;

namespace Salam.Inventory.Application.Services
{
    public class StockAdjustmentManagementService : IStockAdjustmentManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        public StockAdjustmentManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task CreateStockAdjustment_Async(StockAdjustment stockAdjustment, List<StockAdjustmentProduct> stockAdjustmentProducts)
        {
            await UpdateProductWarehouse(stockAdjustmentProducts, stockAdjustment.WarehouseId, stockAdjustment.AdjustmentDate);
            await _inventoryUnitOfWork.StockAdjustmentRepository.AddAsync(stockAdjustment);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task CreateStockAdjustmentReason_Async(StockAdjustmentReason reason)
        {
            await _inventoryUnitOfWork.StockAdjustmentReasonRepository.AddAsync(reason);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task DeleteStockAdjustment_Async(Guid id)
        {
            var stockAdjustment = await _inventoryUnitOfWork.StockAdjustmentRepository
                        .GetStockAdjustmentByIdWithProductsAsync(id);

            var stockAdjustmentProducts = stockAdjustment?.StockAdjustmentProducts?.ToList();

            if(stockAdjustmentProducts?.Count > 0)
            {
                foreach (var product in stockAdjustmentProducts)
                {
                    var productWarehouse = await _inventoryUnitOfWork.ProductWarehouseRepository
                        .GetByIdAsync(product.ProductId, stockAdjustment.WarehouseId);

                    if (productWarehouse != null)
                    {
                        if (product.IsIncrease)
                        {
                            productWarehouse.StockQuantity -= product.AdjustedQuantity;
                            await _inventoryUnitOfWork.ProductWarehouseRepository.EditAsync(productWarehouse);
                        }
                        else
                        {
                            productWarehouse.StockQuantity += product.AdjustedQuantity;
                            await _inventoryUnitOfWork.ProductWarehouseRepository.EditAsync(productWarehouse);
                        }
                    }
                    else
                    {
                        throw new Exception("Warehouse not found...");
                    }
                }
            }
            
            await _inventoryUnitOfWork.StockAdjustmentProductRepository
                                        .RemoveRange_Async(stockAdjustmentProducts);
            await _inventoryUnitOfWork.StockAdjustmentRepository.RemoveAsync(stockAdjustment);

            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task<IList<StockAdjustmentReason>> GetStockAdjustmentReasons_Async()
        {
            return await _inventoryUnitOfWork.StockAdjustmentReasonRepository.GetAllAsync();
        }

        public async Task<(IList<StockAdjustment> data, int total, int totalDisplay)> GetStockAdjustments_Async(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _inventoryUnitOfWork.StockAdjustmentRepository
                    .GetPagedStockAdjustmentList_Async(pageIndex, pageSize, search, order);
        }

        private async Task UpdateProductWarehouse(List<StockAdjustmentProduct> stockAdjustmentProducts, Guid warehouseId, DateTime adjustmentDate)
        {
            foreach (var product in stockAdjustmentProducts)
            {
                var productWarehouse = await _inventoryUnitOfWork.ProductWarehouseRepository
                    .GetByIdAsync(product.ProductId, warehouseId);

                if (productWarehouse != null)
                {
                    if (product.IsIncrease)
                    {
                        productWarehouse.StockQuantity += product.AdjustedQuantity;
                        await _inventoryUnitOfWork.ProductWarehouseRepository.EditAsync(productWarehouse);
                    }
                    else
                    {
                        productWarehouse.StockQuantity -= product.AdjustedQuantity;
                        await _inventoryUnitOfWork.ProductWarehouseRepository.EditAsync(productWarehouse);
                    }
                }
                else
                {
                    var productInfo = await _inventoryUnitOfWork.ProductRepository
                                        .GetProductByIdAsync(product.ProductId);

                    var newProductWarehouse = new ProductWarehouse
                    {
                        ProductId = product.ProductId,
                        WarehouseId = warehouseId,
                        StockQuantity = 0,
                        PerUnitCost = productInfo.BuyingPrice,
                        AsOfDate = adjustmentDate
                    };

                    newProductWarehouse.StockQuantity = product.IsIncrease == true ? (newProductWarehouse.StockQuantity + product.AdjustedQuantity) : (newProductWarehouse.StockQuantity - product.AdjustedQuantity);

                    await _inventoryUnitOfWork.ProductWarehouseRepository
                        .AddAsync(newProductWarehouse);
                }
            }
        }

    }
}