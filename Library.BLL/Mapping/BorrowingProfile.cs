using AutoMapper;
using Library.BLL.DTOs.BorrowingDTO;

namespace Library.BLL.Mapping
{
    public class BorrowingProfile : Profile
    {
        public BorrowingProfile()
        {
            CreateMap<Borrowing, BorrowingDto>()
                .ForMember(
                    d => d.BookTitle,
                    o => o.MapFrom(s => s.Book.Title))
                .ForMember(
                    d => d.MemberName,
                    o => o.MapFrom(s => s.Member.FullName));

            CreateMap<Borrowing, BorrowingDetailsDto>()
                .ForMember(
                    d => d.BookTitle,
                    o => o.MapFrom(s => s.Book.Title))
                .ForMember(
                    d => d.ISBN,
                    o => o.MapFrom(s => s.Book.ISBN))
                .ForMember(
                    d => d.MemberName,
                    o => o.MapFrom(s => s.Member.FullName));

            CreateMap<CreateBorrowingDto, Borrowing>();
        }
    }
}