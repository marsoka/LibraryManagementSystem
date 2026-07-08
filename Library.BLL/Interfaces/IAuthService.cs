using Library.BLL.DTOs;
using Library.BLL.DTOs.AuthDTO;
using Library.Domain.Responses;

namespace Library.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(LoginDto dto);
        Task RegisterUser(RegisterDto dto);
    }
}