

namespace Salam.Inventory.Domain.Entities
{
    public class Tax : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string TaxName { get; set; }
        public double Parcentage { get; set; }
    }
}