namespace cars_api.Models.ModelsDTO
{
    public class CarCrateDTO
    {
        public string Name { get; set; } = null!;
        public string InnerInfo { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime ProductionYear { get; set; }

        public static explicit operator CarCrateDTO(Car car)
        {
            return new CarCrateDTO
            {
                Name = car.Name,
                InnerInfo = car.InnerInfo,
                Description = car.Description,
                ProductionYear = car.ProductionYear
            };
        }
        public static implicit operator Car(CarCrateDTO car)
        {
            return new Car
            {
                Name = car.Name,
                InnerInfo = car.InnerInfo,
                Description = car.Description,
                ProductionYear = car.ProductionYear
            };
        }
    }
}
