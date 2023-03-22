using Dapper;
using System.Data;

namespace cars_api.Db
{
    public class DbInitializer
    {
        private readonly IDbConnection connection;

        public DbInitializer(IDbConnection connection)
        {
            this.connection = connection;
        }
        public  void Initialize()
        {
          var x =  connection.Execute("IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'carsDb')" +
                "BEGIN" +
                "    CREATE DATABASE carsDb" +
                "END");
        }
    }
}
