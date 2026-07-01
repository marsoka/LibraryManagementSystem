using AutoMapper;
using Library.BLL.DTOs.CategoryDTO;

namespace Library.BLL.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();

            CreateMap<CreateCategoryDto, Category>();

            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}