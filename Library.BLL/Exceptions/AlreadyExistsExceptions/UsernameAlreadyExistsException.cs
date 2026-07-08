using Library.BLL.Exceptions.StatusCodesExeptions;

namespace Library.BLL.Exceptions.AlreadyAlreadyExistsException
{
    public class UsernameAlreadyExistsException : ConflictException
    {
        public UsernameAlreadyExistsException(string username)
            : base($"Username '{username}' already exists.")
        {
        }
    }
}