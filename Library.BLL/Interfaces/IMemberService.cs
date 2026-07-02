using Library.BLL.DTOs.MemberDTO;

namespace Library.BLL.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDto>> GetMembersAsync();

        Task<MemberDto?> GetMemberAsync(int id);

        Task CreateMemberAsync(CreateMemberDto dto);

        Task UpdateMemberAsync(int id, UpdateMemberDto dto);

        Task DeleteAuthorAsync(int id);
    }
}
