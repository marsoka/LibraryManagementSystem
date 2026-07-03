using Library.BLL.Exceptions.StatusCodesExeptions;

namespace Library.BLL.Exceptions.BusinessRuleExceptions
{
    public class BookAlreadyReturned : BusinessRuleException
    {
        public BookAlreadyReturned(int id)
            : base($"The book with ID {id} already returned.")
        {
        }
    }
}