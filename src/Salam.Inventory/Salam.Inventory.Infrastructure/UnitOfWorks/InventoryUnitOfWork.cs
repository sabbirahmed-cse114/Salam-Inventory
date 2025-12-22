using Salam.Inventory.Application;
using Salam.Inventory.Domain.Dtos;
using Salam.Inventory.Domain.RepositoryContracts;

namespace Salam.Inventory.Infrastructure.UnitOfWorks
{
    public class InventoryUnitOfWork : UnitOfWork, IInventoryUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; private set; }
        public IMeasurementUnitRepository MeasurementUnitRepository { get; private set; }
        public ITaxRepository TaxRepository { get; private set; }
        public IServiceRepository ServiceRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }
        public IWarehouseRepository WarehouseRepository { get; private set; }
        public IProductWarehouseRepository ProductWarehouseRepository { get; private set; }
        public IStockTransferRepository StockTransferRepository { get; private set; }
        public IStockTransferProductRepository StockTransferProductRepository { get; private set; }
        public IStockAdjustmentReasonRepository StockAdjustmentReasonRepository { get; private set; }
        public IStockAdjustmentProductRepository StockAdjustmentProductRepository { get; private set; }
        public IStockAdjustmentRepository StockAdjustmentRepository { get; private set; }

        public InventoryUnitOfWork(
                ApplicationDbContext dbContext,
                ICategoryRepository categoryRepository,
                IMeasurementUnitRepository measurementUnitRepository,
                ITaxRepository taxRepository,
                IServiceRepository serviceRepository,
                IProductRepository productRepository,
                IWarehouseRepository warehouseRepository,
                IProductWarehouseRepository productWarehouseRepository,
                IStockTransferRepository stockTransferRepository,
                IStockTransferProductRepository stockTransferProductRepository,
                IStockAdjustmentReasonRepository stockAdjustmentReasonRepository,
                IStockAdjustmentProductRepository stockAdjustmentProductRepository,
                IStockAdjustmentRepository stockAdjustmentRepository
                ) : base(dbContext)
        {
            CategoryRepository = categoryRepository;
            MeasurementUnitRepository = measurementUnitRepository;
            TaxRepository = taxRepository;
            ServiceRepository = serviceRepository;
            ProductRepository = productRepository;
            WarehouseRepository = warehouseRepository;
            ProductWarehouseRepository = productWarehouseRepository;
            StockTransferRepository = stockTransferRepository;
            StockTransferProductRepository = stockTransferProductRepository;
            StockAdjustmentReasonRepository = stockAdjustmentReasonRepository;
            StockAdjustmentProductRepository = stockAdjustmentProductRepository;
            StockAdjustmentRepository = stockAdjustmentRepository;
        }

        public async Task<(IList<StockListDto> data, int total, int totalDisplay)> GetPagedStockList_Async(int pageIndex, int pageSize, StockListSearchDto search, string? order)
        {
            var procedureName = "GetStockList";
            var result = await SqlUtility.QueryWithStoredProcedure_Async<StockListDto>
                (procedureName,
                new Dictionary<string, object>
                {
                    { "PageIndex", pageIndex },
                    { "PageSize", pageSize },
                    { "OrderBy", order },
                    { "ItemName", string.IsNullOrEmpty(search.ProductName) ?
                        null : search.ProductName},
                    { "Barcode", string.IsNullOrEmpty(search.Barcode) ?
                        null : search.Barcode},
                    { "CategoryId", string.IsNullOrEmpty(search.CategoryId) ?
                        null: Guid.Parse(search.CategoryId)},
                    { "WarehouseId", string.IsNullOrEmpty(search.WarehouseId) ?
                        null: Guid.Parse(search.WarehouseId)},
                    { "StockGreaterThanZero", search.StockGreaterThanZero },
                    { "BelowMinimumQuantityOfStock", search.BelowMinimumQuantityOfStock }
                },
                new Dictionary<string, Type>
                {
                    { "Total", typeof(int) },
                    { "TotalDisplay", typeof(int) }
                });

            return (result.result, (int)result.outValues["Total"], (int)result.outValues["TotalDisplay"]);
        }
    }
}