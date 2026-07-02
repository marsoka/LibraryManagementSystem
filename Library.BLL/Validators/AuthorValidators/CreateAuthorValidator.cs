using FluentValidation;

public class CreateAuthorValidator : AbstractValidator<CreateAuthorDto>
{
    public CreateAuthorValidator()
    {
        RuleFor(a => a.FullName)
            .NotEmpty()
                .WithMessage("Name is required.")
            .Matches(@"^[a-zA-Z\s]+$")
                .WithMessage("Name can contain letters only.")
            .MaximumLength(150)
                .WithMessage("Name cannot exceed 150 characters.");

        RuleFor(a => a.Biography)
            .NotEmpty()
            .WithMessage("Biography is required.");

        RuleFor(a => a.DateOfBirth)
            .NotEmpty()
            .WithMessage("Data Of Birth is required.")
            // .LessThan(DateOnly.FromDateTime(DateTime.Today))
            .InclusiveBetween(DateOnly.MinValue, DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Birth date must be in the past.");

        RuleFor(a => a.Nationality)
            .NotEmpty()
            .WithMessage("Nationality is required.")
            .MaximumLength(100)
            .WithMessage("Nationality cannot exceed 100 characters.");

    }
}