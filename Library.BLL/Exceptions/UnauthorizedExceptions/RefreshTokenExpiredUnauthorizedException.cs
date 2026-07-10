using Library.BLL.Exceptions.StatusCodesExeptions;

namespace Library.BLL.Exceptions.UnauthorizedExceptions
{
    public class RefreshTokenExpiredUnauthorizedException : UnauthorizedException
    {
        public RefreshTokenExpiredUnauthorizedException()
            : base($"The Refresh Token Expired.")
        {
        }
    }
}