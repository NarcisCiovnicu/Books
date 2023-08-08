using AutoMapper;
using DataAccess.Entities;
using Domain.DataTransferObjects;

namespace BusinessLogic.Mappings
{
    public class BooksMappingProfile : Profile
    {
        public BooksMappingProfile()
        {
            CreateMap<Author, AuthorDTO>().ReverseMap();

            CreateMap<Book, BookDTO>().ReverseMap();
        }
    }
}
