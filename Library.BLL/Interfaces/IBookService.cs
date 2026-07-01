using Library.BLL.DTOs.BookDTO;

namespace Library.BLL.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetBooksAsync();

        Task<BookDetailsDto?> GetBookAsync(int id);

        Task CreateBookAsync(CreateBookDto dto);

        Task UpdateBookAsync(int id, UpdateBookDto dto);

        Task DeleteBookAsync(int id);
    }
}