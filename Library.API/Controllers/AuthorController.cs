
using Library.BLL.Interfaces;
using Library.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
// [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Librarian)]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _service;

    public AuthorController(IAuthorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<AuthorDto>> Get()
    {
        return await _service.GetAuthorsAsync();
    }

    [HttpGet("{id}")]
    public async Task<AuthorDto> GetById(int id)
    {
        return await _service.GetAuthorAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor(CreateAuthorDto createAuthorDto)
    {
        await _service.CreateAuthorAsync(createAuthorDto);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, UpdateAuthorDto updateAuthorDto)
    {
        await _service.UpdateAuthorAsync(id, updateAuthorDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        await _service.DeleteAuthorAsync(id);
        return Ok();
    }


}