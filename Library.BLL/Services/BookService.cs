using AutoMapper;
using Library.BLL.DTOs.BookDTO;
using Library.BLL.Exceptions.AlreadyAlreadyExistsException;
using Library.BLL.Exceptions.NotFoundExceptions;
using Library.BLL.Interfaces;
using Library.DAL.Repositories.Interfaces;

public class BookService : IBookService
{
    private readonly IBookRepository _repoBook;
    private readonly IAuthorRepository _repoAuthor;
    private readonly ICategoryRepository _repoCategory;
    private readonly IPublisherRepository _repoPublisher;
    private readonly IMapper _mapper;

    public BookService(IBookRepository repo, IAuthorRepository repoAuthor,
        ICategoryRepository repoCategory, IPublisherRepository repoPublisher,
          IMapper mapper)
    {
        _repoBook = repo;
        _repoAuthor = repoAuthor;
        _repoCategory = repoCategory;
        _repoPublisher = repoPublisher;
        _mapper = mapper;
    }
    public async Task CreateBookAsync(CreateBookDto dto)
    {
        if (!await _repoAuthor.AuthorIsExistsAsync(dto.AuthorId))
        {
            throw new AuthorNotFoundException(dto.AuthorId);
        }
        if (!await _repoCategory.CategoryIsExistsAsync(dto.CategoryId))
        {
            throw new CategoryNotFoundException(dto.CategoryId);
        }
        if (!await _repoPublisher.PublisherIsExistsAsync(dto.PublisherId))
        {
            throw new PublisherNotFoundException(dto.PublisherId);
        }
        if (await _repoBook.IsbnIsExistsAsync(dto.ISBN))
        {
            throw new ISBNAlreadyExistsException(dto.ISBN);
        }

        var book = _mapper.Map<Book>(dto);
        book.AvailableCopies = book.TotalCopies;
        await _repoBook.AddBookAsync(book);
    }

    public async Task DeleteBookAsync(int id)
    {
        if (!await _repoBook.BookIsExistsAsync(id))
        {
            throw new BookNotFoundException(id);
        }
        await _repoBook.DeleteBookAsync(id);
    }

    public async Task<BookDetailsDto?> GetBookAsync(int id)
    {
        var book = await _repoBook.GetBookAsync(id);
        if (book == null)
        {
            throw new BookNotFoundException(id);
        }

        return _mapper.Map<BookDetailsDto>(book);
    }

    public async Task<IEnumerable<BookDto>> GetBooksAsync()
    {
        var listOfBook = await _repoBook.GetBooksAsync();
        return _mapper.Map<IEnumerable<BookDto>>(listOfBook);
    }

    public async Task UpdateBookAsync(int id, UpdateBookDto dto)
    {
        var book = await _repoBook.GetBookAsync(id);
        if (book == null)
        {
            throw new BookNotFoundException(id);
        }

        if (!await _repoAuthor.AuthorIsExistsAsync(dto.AuthorId))
        {
            throw new AuthorNotFoundException(dto.AuthorId);
        }
        if (!await _repoCategory.CategoryIsExistsAsync(dto.CategoryId))
        {
            throw new CategoryNotFoundException(dto.CategoryId);
        }
        if (!await _repoPublisher.PublisherIsExistsAsync(dto.PublisherId))
        {
            throw new PublisherNotFoundException(dto.PublisherId);
        }

        _mapper.Map(dto, book);
        await _repoBook.UpdateBookAsync(book);
    }
}