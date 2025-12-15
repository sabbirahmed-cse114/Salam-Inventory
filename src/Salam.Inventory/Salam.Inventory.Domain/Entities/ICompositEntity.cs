namespace Salam.Inventory.Domain.Entities
{
    public interface ICompositEntity<T>
    {
        T ProductId { get; set; }
        T WarehouseId { get; set; }
    }
}
