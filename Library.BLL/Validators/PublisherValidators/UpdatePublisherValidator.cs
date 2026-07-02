using FluentValidation;
using Library.BLL.DTOs.PublisherDTO;

namespace Library.BLL.Validators.PublisherValidators
{
    public class UpdatePublisherValidator
            : AbstractValidator<UpdatePublisherDto>
    {
        public UpdatePublisherValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Publisher name is required.")
                .Matches(@"^[a-zA-Z\s]+$")
                .WithMessage("Name can contain letters only.")
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
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Invalid Phone.")
                .MaximumLength(20)
                .WithMessage("Phone number cannot exceed 20 characters.");
        }
    }
}