using Microsoft.AspNetCore.Http;

namespace Library.BLL.Exceptions.StatusCodesExeptions
{
    public class ConflictException : AppException
    {
        public ConflictException(string message)
            : base(message, StatusCodes.Status409Conflict)
        {
        }
    }
}