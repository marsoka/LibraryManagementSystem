using AutoMapper;
using Library.BLL.DTOs.MemberDTO;

public class MemberProfile : Profile
{
    public MemberProfile()
    {
        CreateMap<Member, MemberDto>();

        CreateMap<CreateMemberDto, Member>();

        CreateMap<UpdateMemberDto, Member>();

    }
}