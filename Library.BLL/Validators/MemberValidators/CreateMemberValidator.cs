
using FluentValidation;
using Library.BLL.DTOs.MemberDTO;

public class CreateMemberValidator : AbstractValidator<CreateMemberDto>
{
    public CreateMemberValidator()
    {
        RuleFor(m => m.FullName)
            .NotEmpty()
                .WithMessage("Member name is required.")
            .Matches(@"^[a-zA-Z\s]+$")
                .WithMessage("Name can contain letters only.")
            .MaximumLength(150)
                .WithMessage("Name cannot exceed 150 characters.");

        RuleFor(m => m.Email)
            .NotEmpty()
                .WithMessage("Member Email is required.")
            .EmailAddress()
                .WithMessage("Invalid email.")
            .MaximumLength(150)
                .WithMessage("Email cannot exceed 150 characters.");

        RuleFor(m => m.Phone)
            .NotEmpty()
                .WithMessage("Member Phone number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Invalid Phone.")
            .MaximumLength(20)
                .WithMessage("Member Phone number cannot exceed 20 characters.");

        RuleFor(m => m.Address)
            .NotEmpty()
                .WithMessage("Member Address is required.")
            .MaximumLength(250)
                .WithMessage("Member Address cannot exceed 250 characters.");

        // RuleFor(m => m.RegistrationDate)
        //     .NotEmpty()
        //         .WithMessage("Member Registration Date is required.");



    }
}
