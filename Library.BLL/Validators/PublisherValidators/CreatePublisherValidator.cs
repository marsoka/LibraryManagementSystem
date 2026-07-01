using FluentValidation;
using Library.BLL.DTOs.PublisherDTO;

namespace Library.BLL.Validators.PublisherValidators
{
    public class CreatePublisherValidator
            : AbstractValidator<CreatePublisherDto>
    {
        public CreatePublisherValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Publisher name is required.")
                .MaximumLength(150)
                .WithMessage("Publisher name cannot exceed 150 characters.");

            RuleFor(p => p.Address)
                .NotEmpty()
                .WithMessage("Address is required.")
                .MaximumLength(250)
                .WithMessage("Address cannot exceed 250 characters.");

            RuleFor(p => p.Phone)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .MaximumLength(20)
                .WithMessage("Phone number cannot exceed 20 characters.");
        }
    }
}