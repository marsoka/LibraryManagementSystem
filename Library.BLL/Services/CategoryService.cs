using AutoMapper;
using Library.BLL.DTOs.CategoryDTO;
using Library.BLL.Exceptions.NotFoundExceptions;
using Library.BLL.Interfaces;
using Library.DAL.Repositories.Interfaces;

namespace Library.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        // private readonly ICategoryRepository _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            // if (!await _unitOfWork.Categories.IsExistsAsync(c => c.Id == id))
            if (category == null)
            {
                throw new CategoryNotFoundException(id);
            }
            await _unitOfWork.Categories.DeleteAsync(category);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var listOfCategory = await _unitOfWork.Categories.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(listOfCategory);
        }

        public async Task<CategoryDto?> GetCategoryAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
            {
                throw new CategoryNotFoundException(id);
            }
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task UpdateCategoryAsync(int id, UpdateCategoryDto dto)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
            {
                throw new CategoryNotFoundException(id);
            }

            _mapper.Map(dto, category);
            await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.CompleteAsync();
        }
    }
}