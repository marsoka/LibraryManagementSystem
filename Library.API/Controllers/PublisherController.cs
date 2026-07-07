
using Library.BLL.DTOs.PublisherDTO;
using Library.BLL.Interfaces;
using Library.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = UserRoles.Admin +","+ UserRoles.Librarian)]
public class PublisherController : ControllerBase
{
    private readonly IPublisherService _service;

    public PublisherController(IPublisherService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<PublisherDto>> Get()
    {
        return await _service.GetPublishersAsync();
    }

    [HttpGet("{id}")]
    public async Task<PublisherDto> GetById(int id)
    {
        return await _service.GetPublisherAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePublisher(CreatePublisherDto createPublisherDto)
    {
        await _service.CreatePublisherAsync(createPublisherDto);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePublisher(int id, UpdatePublisherDto updatePublisherDto)
    {
        await _service.UpdatePublisherAsync(id, updatePublisherDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePublisher(int id)
    {
        await _service.DeletePublisherAsync(id);
        return Ok();
    }
}