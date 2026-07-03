using Library.BLL.Exceptions.StatusCodesExeptions;

namespace Library.BLL.Exceptions.BusinessRuleExceptions
{
    public class BookNotAvailableException : BusinessRuleException
    {
        public BookNotAvailableException(int id)
            : base($"The book with ID {id} has no copies available.")
        {
        }
    }
}