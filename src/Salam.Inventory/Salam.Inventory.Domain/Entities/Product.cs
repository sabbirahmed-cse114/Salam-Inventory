using Salam.Inventory.Domain.Entities;

namespace Salam.Inventory.Domain.Entities
{
    public class Product : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal BuyingPrice {  get; set; }
        public decimal SellingPrice { get; set; }
        public string? Barcode { get; set; }
        public string? Picture { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public int? MinimumStockQuantity { get; set; }
        public Guid? TaxId { get; set; }
        public Tax? Tax { get; set; }
        public Guid? CategoryId { get; set; }
        public ProductCategory? Category { get; set; }
        public Guid MeasurementUnitId { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
        public List<ProductWarehouse>? ProductWarehouses { get; set; }
    }
}