using Microsoft.AspNetCore.Http;

namespace Library.BLL.Exceptions.StatusCodesExeptions
{
    public class UnauthorizedException : AppException
    {
        public UnauthorizedException(string message = "Unauthorized.")
            : base(message, StatusCodes.Status401Unauthorized)
        {
        }
    }
}