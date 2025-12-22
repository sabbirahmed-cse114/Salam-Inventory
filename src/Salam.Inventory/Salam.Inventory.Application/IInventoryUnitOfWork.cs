using Salam.Inventory.Domain;
using Salam.Inventory.Domain.Dtos;
using Salam.Inventory.Domain.RepositoryContracts;

namespace Salam.Inventory.Application
{
    public interface IInventoryUnitOfWork : IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IMeasurementUnitRepository MeasurementUnitRepository { get; }
        public ITaxRepository TaxRepository { get; }
        public IServiceRepository ServiceRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IWarehouseRepository WarehouseRepository { get; }
        public IProductWarehouseRepository ProductWarehouseRepository { get; }
        public IStockTransferRepository StockTransferRepository { get; }
        public IStockTransferProductRepository StockTransferProductRepository { get; }
        public IStockAdjustmentReasonRepository StockAdjustmentReasonRepository { get; }
        public IStockAdjustmentProductRepository StockAdjustmentProductRepository { get; }
        public IStockAdjustmentRepository StockAdjustmentRepository { get; }

        Task<(IList<StockListDto> data, int total, int totalDisplay)> GetPagedStockList_Async(
            int pageIndex, int pageSize, StockListSearchDto search, string? order);
    }
}