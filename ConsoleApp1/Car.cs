using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.Models
{
    public class Car
    {
        //  public int Id { get; set; }

        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
        public string InnerInfo { get; set; } = null!;

        public DateTime ProductionYear { get; set; }
    }
}


