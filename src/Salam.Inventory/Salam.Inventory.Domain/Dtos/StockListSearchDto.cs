namespace Salam.Inventory.Domain.Dtos
{
    public class StockListSearchDto
    {
        public string? ProductName { get; set; }
        public string? Barcode { get; set; }
        public string? CategoryId { get; set; }
        public string? WarehouseId { get; set; }
        public bool StockGreaterThanZero { get; set; }
        public bool BelowMinimumQuantityOfStock { get; set; }
    }
}