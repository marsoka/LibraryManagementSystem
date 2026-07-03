using Library.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL.Repositories.Implementations
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly ApplicationDbContext _context;

        public BorrowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddBorrowingAsync(Borrowing borrowing)
        {
            await _context.Borrowings.AddAsync(borrowing);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> BorrowingIsExistsAsync(int id)
        {
            return await _context.Borrowings.AnyAsync(b => b.Id == id);
        }

        // public async Task DeleteBorrowingAsync(int id)
        // {
        //     var borrowing = await _context.Borrowings.FindAsync(id);
        //     if (borrowing != null)
        //     {
        //         _context.Borrowings.Remove(borrowing);
        //         await _context.SaveChangesAsync();
        //     }
        // }

        public async Task<Borrowing?> GetBorrowingAsync(int id)
        {
            var borrowing = await _context.Borrowings
                                .Include(b => b.Book)
                                .Include(b => b.Member)
                                .FirstOrDefaultAsync(b => b.Id == id);
            return borrowing;
        }

        public async Task<IEnumerable<Borrowing>> GetBorrowingsAsync()
        {
            return await _context.Borrowings
                            .Include(b => b.Book)
                            .Include(b => b.Member)
                            .ToListAsync();
        }

        public async Task<bool> HasActiveBorrowingAsync(int memberId, int bookId)
        {
            return await _context.Borrowings
                .AnyAsync(b => b.MemberId == memberId
                    && b.BookId == bookId
                    && b.Status == BorrowingStatus.Borrowed);
        }

        public async Task UpdateBorrowingAsync(Borrowing borrowing)
        {
            _context.Borrowings.Update(borrowing);
            await _context.SaveChangesAsync();
        }
    }
}