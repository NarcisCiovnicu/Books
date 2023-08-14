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

            var query = "INSERT INTO Books(Title, Description, CoverPhoto) VALUES(@Title, @Description, @CoverPhoto) RETURNING Id";
            var newId = await connection.QuerySingleAsync<int>(query, book);

            await InsertAuthorIdsAsync(connection, transaction, newId, book.Authors);

            transaction.Commit();
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var query = @"SELECT B.Id, B.Title, B.Description, B.CoverPhoto, A.Id, A.Name FROM Books B
                        JOIN Books_Authors BA ON B.id = BA.BookID
                        JOIN Authors A ON A.Id = BA.AuthorId;";

            using var connection = _dbContext.CreateConnection();

            var booksDictionary = new Dictionary<int, Book>();

            var result = await connection.QueryAsync<Book, Author, Book>(query, (bookQuery, authorQuery) =>
            {
                if (booksDictionary.TryGetValue(bookQuery.Id, out Book? book))
                {
                    book.Authors.Add(authorQuery);
                }
                else
                {
                    bookQuery.Authors.Add(authorQuery);
                    booksDictionary.Add(bookQuery.Id, bookQuery);
                }

                return bookQuery;
            }, splitOn: "Id");

            return booksDictionary.Values.ToList();
        }

        public async Task<Book> GetAsync(int id)
        {
            var query = @"SELECT Id, Title, Description, CoverPhoto FROM Books WHERE Id = @id;
                          SELECT Id, Name FROM Books_Authors BA
                                JOIN Authors A ON A.Id = BA.AuthorId
                                WHERE BA.BookId = @id;";

            using var connection = _dbContext.CreateConnection();

            using var multi = await connection.QueryMultipleAsync(query, new { id });

            var book = multi.Read<Book>().Single();
            book.Authors = multi.Read<Author>().ToList();

            return book;
        }

        public async Task UpdateAsync(Book book)
        {
            using var connection = _dbContext.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var query = @"UPDATE Books SET Title = @Title, Description = @Description, CoverPhoto = @CoverPhoto WHERE Id = @BookId;
                          DELETE FROM Books_Authors WHERE BookId = @BookId;";

            await connection.ExecuteAsync(query, new
            {
                book.Title,
                book.Description,
                book.CoverPhoto,
                BookId = book.Id
            });
            await InsertAuthorIdsAsync(connection, transaction, book.Id, book.Authors);

            transaction.Commit();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = _dbContext.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var query = @"DELETE FROM Books_Authors WHERE BookId = @id;
                          DELETE FROM Books WHERE Id = @id;";

            await connection.ExecuteAsync(query, new { id });

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
