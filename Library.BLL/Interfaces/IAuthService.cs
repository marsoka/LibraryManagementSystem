using Library.Domain.Responses;

namespace Library.BLL.Interfaces
{
    public interface IAuthService
    {
        AuthResponse GenerateToken(string username);
    }
}