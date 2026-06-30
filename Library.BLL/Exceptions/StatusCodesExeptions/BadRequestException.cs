
using Microsoft.AspNetCore.Http;

namespace Library.BLL.Exceptions.StatusCodesExeptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException(string message)
            : base(message, StatusCodes.Status400BadRequest)
        {

        }
    }
}