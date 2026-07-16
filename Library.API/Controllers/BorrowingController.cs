using Library.BLL.DTOs.BorrowingDTO;
using Library.BLL.Interfaces;
using Library.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
// [Authorize(Roles = UserRoles.Admin +","+ UserRoles.Librarian)]
public class BorrowingController : ControllerBase
{
    private readonly IBorrowingService _service;

    public BorrowingController(IBorrowingService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<BorrowingDto>> Get()
    {
        return await _service.GetBorrowingsAsync();
    }

    [HttpGet("{id}")]
    public async Task<BorrowingDetailsDto> GetById(int id)
    {
        return await _service.GetBorrowingAsync(id);
    }

    [HttpPost]
    public async Task CreateBrowoing(CreateBorrowingDto dto)
    {
        await _service.BorrowingBookAsync(dto);
    }

    [HttpPut("{id}/return")]
    public async Task ReturnBookBorowed(int id)
    {
        await _service.ReturnBorrowedBook(id);
    }

    // [HttpDelete("{id}")]
    // public async Task DeleteBrowoing(int id)
    // {
    //     await _service.DeleteBorrowingAsync(id);
    // }
}