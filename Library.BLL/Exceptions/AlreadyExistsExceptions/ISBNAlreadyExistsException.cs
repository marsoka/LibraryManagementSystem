using Library.BLL.Exceptions.StatusCodesExeptions;

namespace Library.BLL.Exceptions.AlreadyAlreadyExistsException
{
    public class ISBNAlreadyExistsException : ConflictException
    {
        public ISBNAlreadyExistsException(string isbn)
            : base($"Book ISBN '{isbn}' already exists.")
        {
        }
    }
}