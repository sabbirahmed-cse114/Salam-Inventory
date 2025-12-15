namespace Salam.Inventory.Domain
{
    public struct ProductAjaxSearch
    {
        public string SearchProduct { get; set; }
        public Guid WarehouseId { get; set; }
    }
}
