namespace cars_api.Models
{
    public class Car
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime ProductionYear { get; set; }
    }
}
