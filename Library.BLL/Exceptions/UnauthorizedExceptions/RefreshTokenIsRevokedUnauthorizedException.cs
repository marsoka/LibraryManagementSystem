using Library.BLL.Exceptions.StatusCodesExeptions;

namespace Library.BLL.Exceptions.UnauthorizedExceptions
{
    public class RefreshTokenIsRevokedUnauthorizedException : UnauthorizedException
    {
        public RefreshTokenIsRevokedUnauthorizedException()
            : base($"The Refresh Token is revoked.")
        {
        }
    }
}