using cars_api.Models;
using Dapper;
using Npgsql;
using System.Data;

namespace cars_api.Db
{

    public class CarRepositoryDrapper : ICarRepository
    {
       
        private readonly IDbConnection db = null!;
        public CarRepositoryDrapper(IDbConnection dbConnection)
        {
            this.db = dbConnection;
        }

        private readonly string sqlGetCars = $"SELECT * FROM \"Cars\" LIMIT @count OFFSET @offset";
        public async Task<IEnumerable<Car>> GetAllCarsAsync(int page = 1, int count = 100)
        {
            if (page < 1 || count < 1)
                throw new ArgumentException("page and count must be greater than 0");
            var offset = (page - 1) * count;
            return await db.QueryAsync<Car>(sqlGetCars, new { count, offset });
        }
        public IEnumerable<Car> GetAllCars(int page = 1, int count = 100)
        {
            if (page < 1 || count < 1)
                throw new ArgumentException("page and count must be greater than 0");
            var offset = (page - 1) * count;
            return db.Query<Car>(sqlGetCars, new { count, offset });
        }



        private readonly string sqlQueryGetById = "SELECT * FROM  \"Cars\" WHERE \"Id\" = @idCar";
        public async Task<Car?> GetAsync(Guid idCar)
        {
            return (await db.QueryAsync<Car>(sqlQueryGetById,new { idCar })).FirstOrDefault(); ;
        }
        public Car? Get(Guid idCar)
        {
            return db.QueryFirst<Car>(sqlQueryGetById, new { idCar });
        }


        private readonly string sqlQueryCreate = "INSERT INTO \"Cars\"( \"Name\", \"Description\", \"InnerInfo\", \"ProductionYear\") VALUES(@Name,@Description,@InnerInfo,@ProductionYear)";
        public async Task CreateAsync(Car car)
        {
            car.InnerInfo = "new innerInfo";
            await db.ExecuteAsync(sqlQueryCreate, car);
        }
        public void Create(Car car)
        {
            db.Execute(sqlQueryCreate, car);
        }


        private readonly string sqlQueryUpdate = "UPDATE  \"Cars\" SET \"Name\" = @Name, \"Description\" = @Description,\"InnerInfo\" = @InnerInfo,\"ProductionYear\" = @ProductionYear WHERE \"Id\" = @Id";
        public async Task UpdateAsync(Guid id1, Car car)
        {
            car.Id = id1;
            await db.ExecuteAsync(sqlQueryUpdate, car);
        }
        public void Update(Guid id, Car car)
        {
            db.Execute(sqlQueryUpdate, car);
        }


        private readonly string sqlQueryDelete = $"DELETE FROM \"Cars\" WHERE \"Id\" = @id";
        public async Task<bool> DeleteAsync(Guid id)
        {
            return 1 == await db.ExecuteAsync(sqlQueryDelete, new { id });
        }
        public bool Delete(Guid id)
        {
            return 1 == db.Execute(sqlQueryDelete, new { id }); ;
        }
    }
}


