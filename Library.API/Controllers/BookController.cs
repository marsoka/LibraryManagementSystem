
using Library.BLL.DTOs.BookDTO;
using Library.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _service;

    public BookController(IBookService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<BookDto>> Get()
    {
        return await _service.GetBooksAsync();
    }

    [HttpGet("{id}")]
    public async Task<BookDetailsDto> GetById(int id)
    {
        return await _service.GetBookAsync(id);
    }

    [HttpPost]
    public async Task CreateBook(CreateBookDto createBookDto)
    {
        await _service.CreateBookAsync(createBookDto);
    }

    [HttpPut("{id}")]
    public async Task UpdateBook(int id, UpdateBookDto updateBookDto)
    {
        await _service.UpdateBookAsync(id, updateBookDto);
    }

    [HttpDelete("{id}")]
    public async Task DeleteBook(int id)
    {
        await _service.DeleteBookAsync(id);
    }
}