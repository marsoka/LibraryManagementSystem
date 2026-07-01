using FluentValidation;
using Library.BLL.DTOs.BookDTO;

namespace Library.BLL.Validators.BookValidators
{
    public class CreateBookValidator
        : AbstractValidator<CreateBookDto>
    {
        public CreateBookValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.ISBN)
                .NotEmpty()
                .Length(10, 17);

            RuleFor(x => x.PublicationYear)
                .InclusiveBetween(1500, DateTime.Now.Year);

            RuleFor(x => x.TotalCopies)
                .GreaterThan(0);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.AuthorId)
                .GreaterThan(0);

            RuleFor(x => x.CategoryId)
                .GreaterThan(0);

            RuleFor(x => x.PublisherId)
                .GreaterThan(0);
        }
    }
}