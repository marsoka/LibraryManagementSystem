using FluentValidation;
using Library.BLL.DTOs.AuthDTO;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(r => r.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Length(3, 20).WithMessage("Username must be between 3 and 20 characters.")
            .Matches(@"^[a-zA-Z0-9_]+$")
            .WithMessage("Username can only contain letters, numbers, and underscores (_).");


        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Username is required.")
            .EmailAddress().WithMessage("Invalid email.")
            .MaximumLength(150)
                .WithMessage("Email cannot exceed 150 characters.");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[A-Z]")
                .WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]")
                .WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]")
                .WithMessage("Password must contain at least one number.")
            .Matches(@"[^a-zA-Z0-9]")
                .WithMessage("Password must contain at least one special character (e.g. @, #, $, %, etc.).");

        RuleFor(r => r.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required.")
            .Equal(r => r.Password)
                .WithMessage("Passwords do not match.");

    }
}