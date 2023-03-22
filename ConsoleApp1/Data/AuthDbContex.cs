using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;


namespace ConsoleApp1.Data
{

    public class AuthDbContex : DbContext
    {
       private readonly string connectionString = "Host=localhost;Port=5432;Database=carsDb;Username=postgres;Password=3322";
        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Car2> Cars2 { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
