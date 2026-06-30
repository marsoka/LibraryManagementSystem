using Microsoft.AspNetCore.Http;

namespace Library.BLL.Exceptions.StatusCodesExeptions
{
    public class BusinessRuleException : AppException
    {
        public BusinessRuleException(string message)
            : base(message, StatusCodes.Status422UnprocessableEntity)
        {
        }
    }
}