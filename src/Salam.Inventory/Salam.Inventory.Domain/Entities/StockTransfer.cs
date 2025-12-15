namespace Salam.Inventory.Domain.Entities
{
    public class StockTransfer : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid FromWarehouseId { get; set; }
        public Warehouse FromWarehouse { get; set; }
        public Guid ToWarehouseId { get; set; }
        public Warehouse ToWarehouse { get; set; }
        public DateTime DateOfTransfer { get; set; }
        public string? Note { get; set; }
        public List<StockTransferProduct> StockTransferProducts { get; set; }
        public string UserName { get; set; }
    }
}
