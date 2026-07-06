using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Library.BLL.DTOs;
using Library.BLL.Exceptions.StatusCodesExeptions;
using Library.BLL.Interfaces;
using Library.Domain;
using Library.Domain.Responses;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Library.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUserService _userService;

        public AuthService(IOptions<JwtSettings> jwtSettings,
            IUserService userService)
        {
            _jwtSettings = jwtSettings.Value;
            _userService = userService;
        }

        public bool CheckUsernameAndPassword(User? user, LoginDto loginDto)
        {
            if (user == null)
                throw new UnauthorizedException();

            return (user.Username == loginDto.Username) && (user.Password == loginDto.Password);
        }

        public AuthResponse GenerateToken(User user)
        {

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Key!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new AuthResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

        public async Task<AuthResponse> Login(LoginDto dto)
        {
            var user = await _userService.GetUserAsync(dto.Username);

            if (!CheckUsernameAndPassword(user, dto))
                throw new UnauthorizedException();

            return GenerateToken(user!);
        }
    }
}