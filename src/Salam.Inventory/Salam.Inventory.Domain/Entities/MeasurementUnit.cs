namespace Salam.Inventory.Domain.Entities
{
    public class MeasurementUnit : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string UnitName { get; set; }
        public string Symbol { get; set; }
    }
}