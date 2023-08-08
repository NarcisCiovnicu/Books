using Dapper;
using DataAccess.Entities;
using System.Data;

namespace DataAccess.Repositories
{
    public interface IBooksRepository : ICrudRepository<int, Book>
    {
        Task<bool> CheckIfBookExistsAsync(int id);
    }

    public class BooksRepository : IBooksRepository
    {
        private readonly BooksDbContext _dbContext;

        public BooksRepository(BooksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Book book)
        {
            using var connection = _dbContext.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var newId = await connection.QuerySingleAsync<int>("INSERT INTO Books(Title, Description, CoverPhoto) VALUES(@Title, @Description, @CoverPhoto) RETURNING Id", book);

            await InsertAuthorIdsAsync(connection, transaction, newId, book.Authors);

            transaction.Commit();
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var queryBooks = "SELECT Id, Title, Description, CoverPhoto FROM Books";
            var queryAuthors = @"SELECT Id, Name FROM Books_Authors BA
                                JOIN Authors A ON A.Id = BA.AuthorId
                                WHERE BA.BookId = @id";

            using var connection = _dbContext.CreateConnection();
            var books = await connection.QueryAsync<Book>(queryBooks);
            foreach (var book in books)
            {
                book.Authors = (await connection.QueryAsync<Author>(queryAuthors, new { id = book.Id })).ToList();
            }

            return books;
        }

        public async Task<Book> GetAsync(int id)
        {
            var queryBook = "SELECT Id, Title, Description, CoverPhoto FROM Books WHERE Id = @id";
            var queryAuthors = @"SELECT Id, Name FROM Books_Authors BA
                                JOIN Authors A ON A.Id = BA.AuthorId
                                WHERE BA.BookId = @id";

            using var connection = _dbContext.CreateConnection();
            var book = await connection.QueryFirstAsync<Book>(queryBook, new { id });
            book.Authors = (await connection.QueryAsync<Author>(queryAuthors, new { id })).ToList();

            return book;
        }

        public async Task UpdateAsync(Book book)
        {
            using var connection = _dbContext.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            await connection.ExecuteAsync("UPDATE Books SET Title = @Title, Description = @Description, CoverPhoto = @CoverPhoto WHERE Id = @Id", book);
            await connection.ExecuteAsync("DELETE FROM Books_Authors WHERE BookId = @id", new { id = book.Id });
            await InsertAuthorIdsAsync(connection, transaction, book.Id, book.Authors);

            transaction.Commit();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = _dbContext.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            await connection.ExecuteAsync("DELETE FROM Books_Authors WHERE BookId = @id", new { id });
            await connection.ExecuteAsync("DELETE FROM Books WHERE Id = @id", new { id });

            transaction.Commit();
        }

        public async Task<bool> CheckIfBookExistsAsync(int id)
        {
            var query = "SELECT 1 FROM Books WHERE Id = @id";
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<bool>(query, new { id });
        }


        private static async Task InsertAuthorIdsAsync(IDbConnection connection, IDbTransaction transaction, int bookId, IList<Author> authors)
        {
            await connection.ExecuteAsync("INSERT INTO Books_Authors(BookId, AuthorId) VALUES(@bookId, @authorId)",
                                          authors.Select(auth => new { bookId, authorId = auth.Id }).ToList(),
                                          transaction);
        }
    }
}
