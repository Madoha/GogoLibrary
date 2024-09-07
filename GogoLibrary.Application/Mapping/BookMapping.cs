using AutoMapper;
using GogoLibrary.Domain.Dto.Book;
using GogoLibrary.Domain.Entities;

namespace GogoLibrary.Application.Mapping;

public class BookMapping : Profile
{
    public BookMapping()
    {
        CreateMap<Book, SearchBookResultDto>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.BookAuthor, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.YearOfPublication, opt => opt.MapFrom(src => src.YearOfPublication))
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.Link, opt => opt.MapFrom(src => src.Link));

        CreateMap<Book, SearchBookDto>().ReverseMap();
    }
}