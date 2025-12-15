namespace Salam.Inventory.Domain.Dtos
{
    public class StockListDto
    {
        public string ProductName { get; set; }
        public string? Barcode { get; set; }
        public string? WarehouseName { get; set; }
        public string? CategoryName { get; set; }
        public double StockQuantity { get; set; }
        public double MinimumQuantityOfStock { get; set; }
        public string Symbol { get; set; }
        public double PerUnitCost { get; set; }
    }
}
