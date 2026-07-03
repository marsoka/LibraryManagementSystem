using FluentValidation;
using Library.BLL.DTOs.BorrowingDTO;

namespace Library.BLL.Validators.BorrowingValidators
{
    public class CreateBorrowingValidator
        : AbstractValidator<CreateBorrowingDto>
    {
        public CreateBorrowingValidator()
        {
            RuleFor(x => x.BookId)
                .GreaterThan(0);

            RuleFor(x => x.MemberId)
                .GreaterThan(0);
        }
    }
}