using FluentValidation;
using Library.BLL.DTOs.CategoryDTO;

namespace Library.BLL.Validators.CategoryValidators
{
    public class UpdateCategoryValidator
        : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Category name is required.")
                .Matches(@"^[a-zA-Z\s]+$")
                .WithMessage("Name can contain letters only.")
                .MaximumLength(100)
                .WithMessage("Category name cannot exceed 100 characters.");

            RuleFor(c => c.Description)
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");
        }
    }
}