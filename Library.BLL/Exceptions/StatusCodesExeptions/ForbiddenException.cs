using Microsoft.AspNetCore.Http;

namespace Library.BLL.Exceptions.StatusCodesExeptions
{
    public class ForbiddenException : AppException
    {
        public ForbiddenException(string message = "Access denied.")
            : base(message, StatusCodes.Status403Forbidden)
        {
        }
    }
}