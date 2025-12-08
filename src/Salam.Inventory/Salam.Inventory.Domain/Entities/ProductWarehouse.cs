

namespace Salam.Inventory.Domain.Entities
{
    public class ProductWarehouse : ICompositEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public double? StockQuantity { get; set; }
        public decimal? PerUnitCost { get; set; }
        public DateTime? AsOfDate { get; set; }
    }
}
