using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccess
{
    public class BooksDbContext
    {
        private readonly string _connectionString;

        public BooksDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MainConnection")!;
        }

        public IDbConnection CreateConnection() => new SqliteConnection(_connectionString);
    }
}
