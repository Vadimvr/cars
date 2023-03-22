//using Npgsql;

using ConsoleApp1.Models;
using Npgsql;

namespace ConsoleApp1
{
    internal class Program
    {

        static void Main()
        {
            Console.WriteLine("Start");
            var connectionString = "Host=localhost;Port=5432;Database=carsDb;Username=postgres;Password=3322";
            ICarRepository context = new CarRepositoryDrapper(new NpgsqlConnection(connectionString));

            var random = new Random();
            var x = $"\"{Guid.NewGuid()}\",\"{"car"}\",\"des\",\"secret12225\",\"{DateTime.Now}\"";
            Console.WriteLine(x);
            Console.WriteLine(x.Replace("\"", "'"));

            for (int i = 0; i < 1000; i++)
            {
                var car = new Car()
                {

                    Name = $"Car {i}",
                    Description = $"Description car {i}",
                    InnerInfo = $"InnerInfo car {i}",
                    ProductionYear = (DateTime.Now).AddYears(-random.Next(1, 40))
                };
                if (i % 100_000 == 0)
                {
                    Console.WriteLine(i / 100_000);
                }

                context.Create(car);
            }


            //var allRecord = context.GetCars(1, int.MaxValue);

            //foreach (var car in allRecord)
            //{
            //    car.Description = $"{car.Description} Update";
            //    context.UpdateAsync(car.Id, car);
            //}

            //foreach (var car in allRecord)
            //{
            //    context.Delete(car.Id);
            //}


            //Console.WriteLine("End");
            //Console.ReadKey();

        }
    }
}