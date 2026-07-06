using Library.BLL.Exceptions.StatusCodesExeptions;

namespace Library.BLL.Exceptions.NotFoundExceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string username)
            : base($"User with user name {username} was not found.")
        {
        }
    }
}