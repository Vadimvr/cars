﻿namespace cars_api.Models.ModelsDTO
{
    public class CarDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime ProductionYear { get; set; }

        public static explicit operator CarDTO(Car car)
        {
            return new CarDTO
            {
                Id = car.Id,
                Name = car.Name,
                Description = car.Description,
                ProductionYear = car.ProductionYear
            };
        }
        public static implicit operator Car(CarDTO car)
        {
            return new Car
            {
               Id = car.Id,
                Name = car.Name,
                Description = car.Description,
                ProductionYear = car.ProductionYear
            };
        }
    }
}
