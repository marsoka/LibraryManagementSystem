using AutoMapper;
using Library.BLL.DTOs.AuthDTO;

namespace Library.BLL.Mapping
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterDto, User>();
        }
    }
}