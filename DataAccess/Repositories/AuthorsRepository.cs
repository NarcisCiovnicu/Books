using Dapper;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public interface IAuthorsRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<bool> CheckIfAuthorsExistsAsync(IList<int> ids);
    }

    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly BooksDbContext _dbContext;

        public AuthorsRepository(BooksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            var query = "SELECT Id, Name FROM Authors";

            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<Author>(query);
        }

        public async Task<bool> CheckIfAuthorsExistsAsync(IList<int> ids)
        {
            var query = "SELECT COUNT(Id) FROM Authors WHERE Id in @ids";

            using var connection = _dbContext.CreateConnection();
            var count = await connection.QueryFirstOrDefaultAsync<int>(query, new { ids });

            return count == ids.Count;
        }
    }
}
