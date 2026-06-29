using AutoMapper;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<Author, AuthorDto>();

        CreateMap<CreateAuthorDto, Author>();

        CreateMap<UpdateAuthorDto, Author>();

        //CreateMap<Author, AuthorDetailsDto>();
    }
}