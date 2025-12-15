namespace Salam.Inventory.Domain.Entities
{
    public class StockAdjustmentReason : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string ReasonName { get; set; }
    }
}
