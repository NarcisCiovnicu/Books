using BusinessLogic.Mappings;
using BusinessLogic.Services;
using DataAccess.Repositories;
using Domain.CustomExceptions;
using Domain.DataTransferObjects;

namespace BusinessLogic.UnitTests.Services
{
    public class BooksServiceTests
    {
        private readonly IBooksService _bookService;
        private readonly Mock<IBooksRepository> _mockBooksRepository = new();
        private readonly Mock<IAuthorsRepository> _mockAuthorsRepository = new();

        public BooksServiceTests()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile(new BooksMappingProfile());
            });
            var mapper = config.CreateMapper();

            _bookService = new BooksService(mapper, _mockBooksRepository.Object, _mockAuthorsRepository.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldThrowValidationExecption_IfProvidedIdIsNotPartOfCollection()
        {
            // Setup
            _mockBooksRepository.Setup(m => m.CheckIfBookExistsAsync(5)).ReturnsAsync(false);

            // Act
            var action = async () => await _bookService.GetAsync(5);

            // Assert
            await Assert.ThrowsAsync<ValidationException>(action);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowValidationExecption_IfProvidedIdIsNotPartOfCollection()
        {
            // Setup
            _mockBooksRepository.Setup(m => m.CheckIfBookExistsAsync(5)).ReturnsAsync(false);

            // Act
            var action = async () => await _bookService.DeleteAsync(5);

            // Assert
            await Assert.ThrowsAsync<ValidationException>(action);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowValidationExecption_IfProvidedIdIsNotPartOfCollection()
        {
            // Setup
            var book = new BookDTO(5, "", "", Array.Empty<byte>(), new List<AuthorDTO>());

            _mockBooksRepository.Setup(m => m.CheckIfBookExistsAsync(5)).ReturnsAsync(false);

            // Act
            var action = async () => await _bookService.UpdateAsync(book);

            // Assert
            await Assert.ThrowsAsync<ValidationException>(action);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowValidationExecption_IfAuthorsListIsNotPartOfCollection()
        {
            // Setup
            var authors = new List<AuthorDTO>()
            {
                new AuthorDTO(5, ""),
                new AuthorDTO(6, "")
            };
            var book = new BookDTO(5, "", "", Array.Empty<byte>(), authors);
            _mockBooksRepository.Setup(m => m.CheckIfBookExistsAsync(5)).ReturnsAsync(true);
            _mockAuthorsRepository.Setup(m => m.CheckIfAuthorsExistsAsync(It.IsAny<List<int>>())).ReturnsAsync(false);

            // Act
            var action = async () => await _bookService.UpdateAsync(book);

            // Assert
            await Assert.ThrowsAsync<ValidationException>(action);
        }
    }
}