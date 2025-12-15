namespace Salam.Inventory.Domain.Entities
{
    public class StockTransferProduct : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public double TransferQuantity { get; set; }
        public StockTransfer StockTransfers { get; set; }
        public Product Product { get; set; }
    }
}