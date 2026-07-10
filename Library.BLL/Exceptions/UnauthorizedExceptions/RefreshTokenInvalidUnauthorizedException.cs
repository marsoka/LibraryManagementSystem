using Library.BLL.Exceptions.StatusCodesExeptions;

namespace Library.BLL.Exceptions.UnauthorizedExceptions
{
    public class RefreshTokenInvalidUnauthorizedException : UnauthorizedException
    {
        public RefreshTokenInvalidUnauthorizedException(int id)
            : base($"Refresh Token with id {id} Invalid.")
        {
        }

        public RefreshTokenInvalidUnauthorizedException()
            : base($"Refresh Token Invalid.")
        {
        }
    }
}