
using System.Security.Claims;
using Library.BLL.DTOs.BookDTO;
using Library.BLL.Interfaces;
using Library.Domain.Constants;
using Library.Domain.QueryParameters;
using Library.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BookController : ControllerBase
{
    private readonly IBookService _service;

    public BookController(IBookService service)
    {
        _service = service;
    }

    // [HttpGet]
    // public async Task<IEnumerable<BookDto>> Get()
    // {
    //     return await _service.GetBooksAsync();
    // }

    [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Librarian + "," + UserRoles.Member)]
    [HttpGet]
    public async Task<PagedResponse<BookDto>> GetBooks(
        [FromQuery] BookQueryParametersDto query)
    {
        Console.WriteLine("======================================================");
        Console.WriteLine(User.Identity?.Name);
        Console.WriteLine(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        Console.WriteLine(User.FindFirst(ClaimTypes.Name)?.Value);
        Console.WriteLine(User.FindFirst(ClaimTypes.Email)?.Value);
        Console.WriteLine(User.FindFirst(ClaimTypes.Role)?.Value);
        Console.WriteLine("======================================================");
        return await _service.GetBooksAsync(query);
    }

    [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Librarian)]
    [HttpGet("{id}")]
    public async Task<BookDetailsDto> GetById(int id)
    {
        return await _service.GetBookAsync(id);
    }

    [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Librarian)]
    [HttpPost]
    public async Task CreateBook(CreateBookDto createBookDto)
    {
        await _service.CreateBookAsync(createBookDto);
    }

    [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Librarian)]
    [HttpPut("{id}")]
    public async Task UpdateBook(int id, UpdateBookDto updateBookDto)
    {
        await _service.UpdateBookAsync(id, updateBookDto);
    }

    [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Librarian)]
    [HttpDelete("{id}")]
    public async Task DeleteBook(int id)
    {
        await _service.DeleteBookAsync(id);
    }
}