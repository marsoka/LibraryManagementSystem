namespace Library.BLL.DTOs.CategoryDTO
{
    public class UpdateCategoryDto
    {
        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}