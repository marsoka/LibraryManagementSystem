using AutoMapper;
using Library.BLL.DTOs.BookDTO;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookDto>()
            .ForMember(
                dest => dest.AuthorName,
                opt => opt.MapFrom(src => src.Author.FullName))
            .ForMember(
                dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(
                dest => dest.PublisherName,
                opt => opt.MapFrom(src => src.Publisher.Name));

        CreateMap<Book, BookDetailsDto>()
            .ForMember(
                dest => dest.AuthorName,
                opt => opt.MapFrom(src => src.Author.FullName))
            .ForMember(
                dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(
                dest => dest.PublisherName,
                opt => opt.MapFrom(src => src.Publisher.Name));

        CreateMap<CreateBookDto, Book>();

        CreateMap<UpdateBookDto, Book>();
    }
}