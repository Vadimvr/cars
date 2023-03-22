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

        public async Task<IEnumerable<Car>> GetCarsAsync(int page = 1, int count = 100)
        {
            if (page < 1 || count < 1)
                throw new ArgumentException("page and count must be greater than 0");

            return await db.QueryAsync<Car>($"SELECT * FROM \"Cars\" LIMIT {count} OFFSET {(page - 1) * count}");
        }

        public async Task<Car?> GetAsync(int id)
        {
            IEnumerable<Car> res = await db.QueryAsync<Car>("SELECT * FROM  \"Cars\" WHERE \"Id\" = @id", new { id });
            return res.FirstOrDefault();
        }

        public async Task CreateAsync(Car car)
        {

            var sqlQuery = "INSERT INTO \"Cars\" (\"Guid\",\"Name\", \"Description\",\"ProductionYear\") VALUES(@Guid,@Name,@Description,@ProductionYear)";
            await db.ExecuteAsync(sqlQuery, car);
        }

        public async Task UpdateAsync(int id, Car car)
        {

            var sqlQuery = "UPDATE  \"Cars\" SET \"Guid\" = @Guid,\"Name\" = @name, \"Description\" = @Description,\"ProductionYear\" = @ProductionYear" +
                $" WHERE \"Id\" = {id}";
            await db.ExecuteAsync(sqlQuery, car);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sqlQuery = $"DELETE FROM \"Cars\" WHERE \"Id\" = {id}";
            return 1 == await db.ExecuteAsync(sqlQuery, new { id }); ;

        }

        public async Task<IEnumerable<Car>> GetCarsRangeAsync(int start, int end)
        {
            if (start > end)
            {
                (end, start) = (start, end);
            }
            return await db.QueryAsync<Car>($"SELECT * FROM \"Cars\" WHERE \"Id\" >= {start} AND  \"Id\" <={end}");

        }
    }
}
