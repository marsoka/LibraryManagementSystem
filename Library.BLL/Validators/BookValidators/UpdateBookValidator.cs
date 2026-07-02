using FluentValidation;
using Library.BLL.DTOs.BookDTO;

namespace Library.BLL.Validators.BookValidators
{
    public class UpdateBookValidator
        : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                    .WithMessage("Book name is required.")
                .MaximumLength(200)
                    .WithMessage("Book name cannot exceed 200 characters.");

            RuleFor(x => x.ISBN)
                .NotEmpty()
                    .WithMessage("Book ISBN is required.")
                .Matches(@"^\d{13}$")
                    .WithMessage("ISBN must contain exactly 13 digits.");

            RuleFor(x => x.PublicationYear)
                .InclusiveBetween(1500, DateTime.Now.Year)
                    .WithMessage("Publication year must be in the past.");

            RuleFor(x => x.TotalCopies)
                .GreaterThan(0)
                    .WithMessage("Total copies must be graeter than 0.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                    .WithMessage("Total copies must be graeter than or equal to 0.");

            RuleFor(x => x.AuthorId)
                .GreaterThan(0)
                    .WithMessage("Author ID must be graeter than 0.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                    .WithMessage("Category ID must be graeter than 0.");

            RuleFor(x => x.PublisherId)
                .GreaterThan(0)
                    .WithMessage("Publisher ID must be graeter than 0.");
        }
    }
}