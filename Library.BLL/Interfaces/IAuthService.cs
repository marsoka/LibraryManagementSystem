using Library.BLL.DTOs;
using Library.Domain.Responses;

namespace Library.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(LoginDto dto);
        bool CheckUsernameAndPassword(User? user, LoginDto loginDto);
        AuthResponse GenerateToken(User user);
    }
}