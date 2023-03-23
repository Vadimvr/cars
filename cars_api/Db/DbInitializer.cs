using Dapper;
using System.Data;

namespace cars_api.Db
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IDbConnection connection;
        private readonly ILogger<Program> logger;

        public DbInitializer(IDbConnection connection, ILogger<Program> logger)
        {
            this.connection = connection;
            this.logger = logger;
        }
        public void Initialize()
        {
            var conectionString = connection.ConnectionString;
            var databaseName = connection.Database;
            //
            connection.ConnectionString = connection.ConnectionString.Replace($"Database={databaseName}", string.Empty);
            try
            {
                connection.Execute($"CREATE DATABASE \"{databaseName}\";");
                logger.LogInformation($"Create DATABASE {databaseName}");

            }
            catch (Npgsql.PostgresException ex)
            {
                logger.LogInformation($"Use DATABASE {databaseName}");
            }
            finally
            {
                connection.ConnectionString = conectionString;
            }

            // if use uuid
            try
            {
                connection.Execute($"CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";");
            }
            catch (Npgsql.PostgresException ex)
            {
                logger.LogWarning(ex.Message);
            }
         
            //table
            try
            {
                connection.Execute(
                    $"CREATE TABLE IF NOT EXISTS public.\"Cars\"" +
                    $"(" +
                    $"\"Id\" uuid DEFAULT uuid_generate_v4 ()," +
                    $"\"Name\" text COLLATE pg_catalog.\"default\" NOT NULL," +
                    $"\"Description\" text COLLATE pg_catalog.\"default\"," +
                    $"\"InnerInfo\" text COLLATE pg_catalog.\"default\" NOT NULL," +
                    $"\"ProductionYear\" timestamp with time zone NOT NULL," +
                    $"PRIMARY KEY (\"Id\")" +
                    $");");
            }
            catch (Npgsql.PostgresException ex)
            {
                logger.LogWarning(ex.Message);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }

    public interface IDbInitializer
    {
        void Initialize();
    }
}
