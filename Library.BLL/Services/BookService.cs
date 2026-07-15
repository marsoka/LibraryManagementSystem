using AutoMapper;
using Library.BLL.DTOs.BookDTO;
using Library.BLL.Exceptions.AlreadyAlreadyExistsException;
using Library.BLL.Exceptions.NotFoundExceptions;
using Library.BLL.Interfaces;
using Library.DAL.Repositories.Interfaces;
using Library.Domain.QueryParameters;
using Library.Domain.Responses;

public class BookService : IBookService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BookService(IUnitOfWork unitOfWork,
          IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task CreateBookAsync(CreateBookDto dto)
    {
        if (!await _unitOfWork.Authors.IsExistsAsync(a => a.Id == dto.AuthorId))
        {
            throw new AuthorNotFoundException(dto.AuthorId);
        }
        if (!await _unitOfWork.Categories.IsExistsAsync(c => c.Id == dto.CategoryId))
        {
            throw new CategoryNotFoundException(dto.CategoryId);
        }
        if (!await _unitOfWork.Publishers.IsExistsAsync(p => p.Id == dto.PublisherId))
        {
            throw new PublisherNotFoundException(dto.PublisherId);
        }
        if (await _unitOfWork.Books.IsExistsAsync(b => b.ISBN == dto.ISBN))
        {
            throw new ISBNAlreadyExistsException(dto.ISBN);
        }

        var book = _mapper.Map<Book>(dto);
        book.AvailableCopies = book.TotalCopies;
        await _unitOfWork.Books.AddAsync(book);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteBookAsync(int id)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(id);
        if (book == null)
        {
            throw new BookNotFoundException(id);
        }
        await _unitOfWork.Books.DeleteAsync(book);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<BookDetailsDto?> GetBookAsync(int id)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(id);
        if (book == null)
        {
            throw new BookNotFoundException(id);
        }

        return _mapper.Map<BookDetailsDto>(book);
    }

    // public async Task<IEnumerable<BookDto>> GetBooksAsync()
    // {
    //     var listOfBook = await _repoBook.GetBooksAsync();
    //     return _mapper.Map<IEnumerable<BookDto>>(listOfBook);
    // }


    public async Task<PagedResponse<BookDto>> GetBooksAsync(
        BookQueryParametersDto query)
    {
        var bookPagedResponse = await _unitOfWork.Books.SearchBooksAsync(query);

        return new PagedResponse<BookDto>
        {
            Data = _mapper.Map<IEnumerable<BookDto>>(bookPagedResponse.Data),
            PageNumber = query.PageNumber,
            PageSize = query.PageSize,
            TotalCount = bookPagedResponse.TotalCount,
            TotalPages = (int)Math.Ceiling(
            bookPagedResponse.TotalCount / (double)query.PageSize)
        };
    }

    public async Task UpdateBookAsync(int id, UpdateBookDto dto)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(id);
        if (book == null)
        {
            throw new BookNotFoundException(id);
        }

        if (!await _unitOfWork.Authors.IsExistsAsync(a => a.Id == dto.AuthorId))
        {
            throw new AuthorNotFoundException(dto.AuthorId);
        }
        if (!await _unitOfWork.Categories.IsExistsAsync(c => c.Id == dto.CategoryId))
        {
            throw new CategoryNotFoundException(dto.CategoryId);
        }
        if (!await _unitOfWork.Publishers.IsExistsAsync(p => p.Id == dto.PublisherId))
        {
            throw new PublisherNotFoundException(dto.PublisherId);
        }

        _mapper.Map(dto, book);
        await _unitOfWork.Books.UpdateAsync(book);
        await _unitOfWork.CompleteAsync();
    }
}