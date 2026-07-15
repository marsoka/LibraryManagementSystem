using Library.Domain.QueryParameters;
using Library.Domain.Responses;

namespace Library.DAL.Repositories.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        // Task<IEnumerable<Book>> GetBooksAsync();
        Task<PagedResponse<Book>> SearchBooksAsync(BookQueryParametersDto query);
    }
}