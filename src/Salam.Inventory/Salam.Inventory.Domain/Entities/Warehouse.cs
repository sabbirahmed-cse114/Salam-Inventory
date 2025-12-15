namespace Salam.Inventory.Domain.Entities
{
    public class Warehouse : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string WarehouseName { get; set; }
        public List<ProductWarehouse>? ProductWarehouses { get; set; }
    }
}
