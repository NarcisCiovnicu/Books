using AutoMapper;
using DataAccess.Entities;
using DataAccess.Repositories;
using Domain.CustomExceptions;
using Domain.DataTransferObjects;

namespace BusinessLogic.Services
{
    public interface IBooksService
    {
        Task AddAsync(BookDTO book);
        Task DeleteAsync(int id);
        Task<BookDTO> GetAsync(int id);
        Task<IEnumerable<BookDTO>> GetAllAsync();
        Task UpdateAsync(BookDTO book);
    }

    public class BooksService : IBooksService
    {
        private readonly IMapper _mapper;
        private readonly IBooksRepository _booksRepository;
        private readonly IAuthorsRepository _authorsRepository;

        public BooksService(IMapper mapper, IBooksRepository booksRepository, IAuthorsRepository authorsRepository)
        {
            _mapper = mapper;
            _booksRepository = booksRepository;
            _authorsRepository = authorsRepository;
        }

        public async Task AddAsync(BookDTO book)
        {
            await ValidateAsync(book);

            await _booksRepository.AddAsync(_mapper.Map<Book>(book));
        }

        public async Task<IEnumerable<BookDTO>> GetAllAsync()
        {
            var books = await _booksRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public async Task<BookDTO> GetAsync(int id)
        {
            await ThrowIfNotFoundAsync(id);

            var book = await _booksRepository.GetAsync(id);
            return _mapper.Map<BookDTO>(book);
        }

        public async Task UpdateAsync(BookDTO book)
        {
            await ThrowIfNotFoundAsync(book.Id);
            await ValidateAsync(book);
            await _booksRepository.UpdateAsync(_mapper.Map<Book>(book));
        }

        public async Task DeleteAsync(int id)
        {
            await ThrowIfNotFoundAsync(id);
            await _booksRepository.DeleteAsync(id);
        }

        private async Task ThrowIfNotFoundAsync(int id)
        {
            var result = await _booksRepository.CheckIfBookExistsAsync(id);
            if (result == false)
            {
                throw new ValidationException("This book was not found.");
            }
        }

        private async Task ValidateAsync(BookDTO book)
        {
            var result = await _authorsRepository.CheckIfAuthorsExistsAsync(book.Authors.Select(a => a.Id).ToList());
            if (result == false)
            {
                throw new ValidationException("The book has invalid authors.");
            }
        }
    }
}