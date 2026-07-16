
using Library.BLL.DTOs.CategoryDTO;
using Library.BLL.Interfaces;
using Library.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
// [Authorize(Roles = UserRoles.Admin +","+ UserRoles.Librarian)]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<CategoryDto>> Get()
    {
        return await _service.GetCategoriesAsync();
    }

    [HttpGet("{id}")]
    public async Task<CategoryDto> GetById(int id)
    {
        return await _service.GetCategoryAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        await _service.CreateCategoryAsync(createCategoryDto);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
    {
        await _service.UpdateCategoryAsync(id, updateCategoryDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _service.DeleteCategoryAsync(id);
        return Ok();
    }
}