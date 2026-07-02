using Library.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL.Repositories.Implementations
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;

        public MemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddMemberAsync(Member member)
        {
            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMemberAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Member?> GetMemberAsync(int id)
        {
            return await _context.Members.FindAsync(id);
        }

        public async Task<IEnumerable<Member>> GetMembersAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<bool> MemberIsExistsAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            return member != null;
        }

        public async Task UpdateMemberAsync(Member member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EmailIsExistsAsync(string email)
        {
            return await _context.Members.AnyAsync(m => m.Email == email);
        }

        public async Task<bool> PhoneIsExistsAsync(string phone)
        {
            return await _context.Members.AnyAsync(m => m.Phone == phone);
        }
    }
}