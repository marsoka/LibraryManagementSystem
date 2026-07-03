using Library.BLL.DTOs.BookDTO;
using Library.Domain.QueryParameters;
using Library.Domain.Responses;

namespace Library.BLL.Interfaces
{
    public interface IBookService
    {
        // Task<IEnumerable<BookDto>> GetBooksAsync();
        Task<PagedResponse<BookDto>> GetBooksAsync(BookQueryParametersDto query);


        Task<BookDetailsDto?> GetBookAsync(int id);

        Task CreateBookAsync(CreateBookDto dto);

        Task UpdateBookAsync(int id, UpdateBookDto dto);

        Task DeleteBookAsync(int id);
    }
}