namespace Salam.Inventory.Domain.Dtos
{
    public class LogListDto
    {
        public int Id { get; set; }
        public string LogMessage { get; set; }
        public string Type { get; set; }
        public DateTime EventTime { get; set; }
        public string? Exception { get; set; }
    }
}