using System.ComponentModel.DataAnnotations;

namespace cars_api.Models
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
        public string InnerInfo { get; set; } = null!;

        public DateTime ProductionYear { get; set; }
    }
}
