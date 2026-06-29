using FluentValidation;

public class CreateAuthorValidator : AbstractValidator<CreateAuthorDto>
{
    public CreateAuthorValidator()
    {
        RuleFor(a => a.FullName)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(a => a.Biography)
            .NotEmpty();

        RuleFor(a => a.DateOfBirth)
            .NotEmpty();

        RuleFor(a => a.Nationality)
            .NotEmpty()
            .MaximumLength(100);

    }
}