using Library.DAL.Repositories.Interfaces;
using Library.Domain.QueryParameters;
using Library.Domain.Responses;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL.Repositories.Implementations
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedResponse<Book>> SearchBooksAsync(
            BookQueryParametersDto query)
        {
            IQueryable<Book> books = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Publisher);

            // Search
            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                books = books.Where(b =>
                    b.Title.Contains(query.SearchTerm) ||
                    b.ISBN.Contains(query.SearchTerm));
            }

            // Filters
            if (query.AuthorId.HasValue)
            {
                books = books.Where(
                    b => b.AuthorId == query.AuthorId.Value);
            }

            if (query.CategoryId.HasValue)
            {
                books = books.Where(
                    b => b.CategoryId == query.CategoryId.Value);
            }

            if (query.PublisherId.HasValue)
            {
                books = books.Where(
                    b => b.PublisherId == query.PublisherId.Value);
            }

            if (query.MinPrice.HasValue)
            {
                books = books.Where(
                    b => b.Price >= query.MinPrice.Value);
            }

            if (query.MaxPrice.HasValue)
            {
                books = books.Where(
                    b => b.Price <= query.MaxPrice.Value);
            }

            if (query.PublicationYear.HasValue)
            {
                books = books.Where(
                    b => b.PublicationYear == query.PublicationYear.Value);
            }

            // Sorting
            books = query.SortBy?.ToLower() switch
            {
                "title" => query.Descending
                    ? books.OrderByDescending(b => b.Title)
                    : books.OrderBy(b => b.Title),

                "price" => query.Descending
                    ? books.OrderByDescending(b => b.Price)
                    : books.OrderBy(b => b.Price),

                "year" => query.Descending
                    ? books.OrderByDescending(b => b.PublicationYear)
                    : books.OrderBy(b => b.PublicationYear),

                _ => books.OrderBy(b => b.Id)
            };

            // Pagination
            var totalCount = await books.CountAsync();

            var item = await books
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return new PagedResponse<Book>
            {
                Data = item,
                TotalCount = totalCount
            };
        }

    }
}