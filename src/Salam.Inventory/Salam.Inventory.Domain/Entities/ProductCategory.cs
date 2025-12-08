

namespace Salam.Inventory.Domain.Entities
{
    public class ProductCategory : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<Product>? Products { get; set; }
    }
}
