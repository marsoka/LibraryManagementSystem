using Library.BLL.Exceptions.StatusCodesExeptions;

namespace Library.BLL.Exceptions.AlreadyAlreadyExistsException
{
    public class PhoneAlreadyExistsException : ConflictException
    {
        public PhoneAlreadyExistsException(string phone)
            : base($"Phone '{phone}' already exists.")
        {
        }
    }
}