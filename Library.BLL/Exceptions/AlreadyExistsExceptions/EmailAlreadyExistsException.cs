using Library.BLL.Exceptions.StatusCodesExeptions;

namespace Library.BLL.Exceptions.EmailAlreadyExistsExceptions
{
    public class EmailAlreadyExistsException : ConflictException
    {
        public EmailAlreadyExistsException(string email)
            : base($"Email '{email}' already exists.")
        {
        }
    }
}