namespace Salam.Inventory.Domain.Dtos
{
    public class ProductSearchDto
    {
        public string? ProductName { get; set; }
        public string? Barcode { get; set; }
        public string? CategoryId { get; set; }
        public string? WarehouseId { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public bool? IsActive { get; set; }
        public bool BelowMinimumQuantityOfStock { get; set; }

    }
}
