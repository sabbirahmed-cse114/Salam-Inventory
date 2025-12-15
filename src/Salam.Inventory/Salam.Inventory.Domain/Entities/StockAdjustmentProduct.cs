namespace Salam.Inventory.Domain.Entities
{
    public class StockAdjustmentProduct : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public double AdjustedQuantity { get; set; }
        public bool IsIncrease { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public StockAdjustment StockAdjustments { get; set; }
    }
}