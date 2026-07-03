namespace Library.DAL.Repositories.Interfaces
{
    public interface IBorrowingRepository
    {
        Task<IEnumerable<Borrowing>> GetBorrowingsAsync();
        Task<Borrowing?> GetBorrowingAsync(int id);
        Task AddBorrowingAsync(Borrowing borrowing);
        Task UpdateBorrowingAsync(Borrowing borrowing);
        // Task DeleteBorrowingAsync(int id);
        Task<bool> BorrowingIsExistsAsync(int id);
        Task<bool> HasActiveBorrowingAsync(int memberId, int bookId);

    }
}