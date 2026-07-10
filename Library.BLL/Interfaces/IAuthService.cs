using System.Security.Claims;
using Library.BLL.DTOs;
using Library.BLL.DTOs.AuthDTO;
using Library.Domain.Responses;

namespace Library.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(LoginDto dto);
        Task Logout(ClaimsPrincipal user, LogoutRequest logoutRequest);
        Task RegisterUser(RegisterDto dto);
        Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
    }
}