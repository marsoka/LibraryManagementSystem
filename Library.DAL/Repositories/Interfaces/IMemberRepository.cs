namespace Library.DAL.Repositories.Interfaces
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetMembersAsync();
        Task<Member?> GetMemberAsync(int id);
        Task AddMemberAsync(Member member);
        Task UpdateMemberAsync(Member member);
        Task DeleteMemberAsync(int id);
        Task<bool> MemberIsExistsAsync(int id);
        Task<bool> EmailIsExistsAsync(string email);
        Task<bool> PhoneIsExistsAsync(string phone);


    }
}