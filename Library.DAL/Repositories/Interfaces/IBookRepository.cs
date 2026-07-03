using Library.Domain.QueryParameters;
using Library.Domain.Responses;

namespace Library.DAL.Repositories.Interfaces
{
    public interface IBookRepository
    {
        // Task<IEnumerable<Book>> GetBooksAsync();
        Task<PagedResponse<Book>> GetBooksAsync(BookQueryParametersDto query);
        Task<Book?> GetBookAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task<bool> BookIsExistsAsync(int id);
        Task<bool> IsbnIsExistsAsync(string isbn);
    }
}