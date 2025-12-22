using Salam.Inventory.Domain.Entities;
using Salam.Inventory.Domain;

namespace Salam.Inventory.Application.Services
{
    public class StockTransferManagementService : IStockTransferManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        public StockTransferManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }
        public async Task CreateStockTransfer_Async(StockTransfer stockTransfer, List<StockTransferProduct> stockTransferProducts)
        {
            await UpdateProductWarehouseAsync(stockTransferProducts, stockTransfer.DateOfTransfer, stockTransfer.FromWarehouseId, stockTransfer.ToWarehouseId);
            await _inventoryUnitOfWork.StockTransferRepository.AddAsync(stockTransfer);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task DeleteStockTransfer_Async(Guid id)
        {
            var stockTransfer = await _inventoryUnitOfWork.StockTransferRepository.GetStockTransferWithProductsAsync(id);

            var stockTransferProducts = stockTransfer.StockTransferProducts.ToList();

            foreach (var product in stockTransferProducts)
            {
                var productWarehouse = await _inventoryUnitOfWork.ProductWarehouseRepository
                    .GetByIdAsync(product.ProductId, stockTransfer.FromWarehouseId);

                if (productWarehouse != null)
                {
                    productWarehouse.StockQuantity += product.TransferQuantity;
                    await _inventoryUnitOfWork.ProductWarehouseRepository.EditAsync(productWarehouse);

                    var toProductWarehouse = await _inventoryUnitOfWork.ProductWarehouseRepository
                    .GetByIdAsync(product.ProductId, stockTransfer.ToWarehouseId);

                    if (toProductWarehouse != null)
                    {
                        toProductWarehouse.StockQuantity -= product.TransferQuantity;
                        await _inventoryUnitOfWork.ProductWarehouseRepository
                            .EditAsync(toProductWarehouse);
                    }
                }
            }
            await _inventoryUnitOfWork.StockTransferProductRepository.RemoveRange_Async(stockTransferProducts);
            await _inventoryUnitOfWork.StockTransferRepository.RemoveAsync(stockTransfer);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task<StockTransfer> GetStockTransferProducts_Async(Guid id)
        {
            return await _inventoryUnitOfWork.StockTransferRepository.GetStockTransferWithProductsAsync(id);
        }

        public async Task<(IList<StockTransfer> data, int total, int totalDisplay)> GetStockTransfers_Async(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _inventoryUnitOfWork.StockTransferRepository.GetPagedStockTransfersAsync(pageIndex, pageSize, search, order);
        }

        private async Task UpdateProductWarehouseAsync(List<StockTransferProduct> stockTransferProducts, DateTime transferDate, Guid fromWarehouseId, Guid toWarehouseId)
        {
            foreach (var product in stockTransferProducts)
            {
                var productWarehouse = await _inventoryUnitOfWork.ProductWarehouseRepository
                    .GetByIdAsync(product.ProductId, fromWarehouseId);

                if(productWarehouse != null)
                {
                    if(productWarehouse.StockQuantity >= product.TransferQuantity)
                    {
                        productWarehouse.StockQuantity -= product.TransferQuantity;
                        await _inventoryUnitOfWork.ProductWarehouseRepository.EditAsync(productWarehouse);

                        var toProductWarehouse = await _inventoryUnitOfWork.ProductWarehouseRepository
                        .GetByIdAsync(product.ProductId, toWarehouseId);

                        if (toProductWarehouse != null)
                        {
                            toProductWarehouse.StockQuantity += product.TransferQuantity;
                            await _inventoryUnitOfWork.ProductWarehouseRepository
                                .EditAsync(toProductWarehouse);
                        }
                        else
                        {
                            var newProductWarehouse = new ProductWarehouse
                            {
                                ProductId = product.ProductId,
                                WarehouseId = toWarehouseId,
                                StockQuantity = product.TransferQuantity,
                                PerUnitCost = productWarehouse?.PerUnitCost,
                                AsOfDate = transferDate,
                            };

                            await _inventoryUnitOfWork.ProductWarehouseRepository
                                .AddAsync(newProductWarehouse);
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("Quantity can not be bigger than Available Stock...");
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Product warehouse not found");
                }
            }
        }
    }
}