

namespace Salam.Inventory.Domain.Entities
{
    public class Service : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double? BuyingPrice { get; set; }
        public double? SellingPrice { get; set; }
        public string? Description { get; set; }
        public Guid? TaxId { get; set; }
        public Tax? Tax { get; set; }
    }
}
