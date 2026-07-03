using Library.BLL.DTOs.BorrowingDTO;

namespace Library.BLL.Interfaces
{
    public interface IBorrowingService
    {
        Task<IEnumerable<BorrowingDto>> GetBorrowingsAsync();

        Task<BorrowingDetailsDto?> GetBorrowingAsync(int id);

        Task BorrowingBookAsync(CreateBorrowingDto dto);
        Task ReturnBorrowedBook(int id);

        // Task DeleteBorrowingAsync(int id);

    }
}