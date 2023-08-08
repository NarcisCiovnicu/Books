using AutoMapper;
using DataAccess.Repositories;
using Domain.DataTransferObjects;

namespace BusinessLogic.Services
{
    public interface IAuthorsService
    {
        Task<IEnumerable<AuthorDTO>> GetAllAsync();
    }

    public class AuthorsService : IAuthorsService
    {
        private readonly IMapper _mapper;
        private readonly IAuthorsRepository _authorsRepository;

        public AuthorsService(IMapper mapper, IAuthorsRepository authorsRepository)
        {
            _mapper = mapper;
            _authorsRepository = authorsRepository;
        }

        public async Task<IEnumerable<AuthorDTO>> GetAllAsync()
        {
            var authors = await _authorsRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorDTO>>(authors);
        }
    }
}
