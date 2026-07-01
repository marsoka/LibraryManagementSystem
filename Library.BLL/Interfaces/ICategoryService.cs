using Library.BLL.DTOs.CategoryDTO;

namespace Library.BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();

        Task<CategoryDto?> GetCategoryAsync(int id);

        Task CreateCategoryAsync(CreateCategoryDto dto);

        Task UpdateCategoryAsync(int id, UpdateCategoryDto dto);

        Task DeleteCategoryAsync(int id);
    }
}