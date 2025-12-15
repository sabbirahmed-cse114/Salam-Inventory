namespace Salam.Inventory.Domain.Dtos
{
    public class ProductListDto
    {
        public Guid Id { get; set; }
        public string? Picture { get; set; }
        public string ProductName { get; set; }
        public string? Barcode { get; set; }
        public string? Category { get; set; }
        public double Price { get; set; }
        public double? Tax { get; set; }
        public int? MinimumQuantityOfStock { get; set; }
        public double? TotalQuantityOfStock { get; set; }
        public string? MeasurementUnit { get; set; }
        public bool IsActive { get; set; }
    }
}