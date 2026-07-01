using AutoMapper;
using Library.BLL.DTOs.CategoryDTO;
using Library.BLL.Exceptions.NotFoundExceptions;
using Library.BLL.Interfaces;
using Library.DAL.Repositories.Interfaces;

namespace Library.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _repo.AddCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            if (!await _repo.CategoryIsExistsAsync(id))
            {
                throw new CategoryNotFoundException(id);
            }
            await _repo.DeleteCategoryAsync(id);
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var listOfCategory = await _repo.GetCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(listOfCategory);
        }

        public async Task<CategoryDto?> GetCategoryAsync(int id)
        {
            var category = await _repo.GetCategoryAsync(id);
            if (category == null)
            {
                throw new CategoryNotFoundException(id);
            }
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task UpdateCategoryAsync(int id, UpdateCategoryDto dto)
        {
            var category = await _repo.GetCategoryAsync(id);
            if (category == null)
            {
                throw new CategoryNotFoundException(id);
            }

            _mapper.Map(dto, category);
            await _repo.UpdateCategoryAsync(category);
        }
    }
}