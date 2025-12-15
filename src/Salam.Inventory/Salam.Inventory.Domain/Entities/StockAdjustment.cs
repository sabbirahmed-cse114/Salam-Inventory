namespace Salam.Inventory.Domain.Entities
{
    public class StockAdjustment : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public Guid WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
        public Guid StockAdjustmentReasonId { get; set; }
        public StockAdjustmentReason? ReasonOfStockAdjustment { get; set; }
        public string? Note { get; set; }
        public List<StockAdjustmentProduct>? StockAdjustmentProducts { get; set; }
        public string UserName { get; set; }
    }
}
