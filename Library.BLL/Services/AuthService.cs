using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Library.BLL.DTOs;
using Library.BLL.DTOs.AuthDTO;
using Library.BLL.Exceptions.AlreadyAlreadyExistsException;
using Library.BLL.Exceptions.StatusCodesExeptions;
using Library.BLL.Interfaces;
using Library.DAL.Repositories.Interfaces;
using Library.Domain;
using Library.Domain.Constants;
using Library.Domain.Responses;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Library.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthService(IOptions<JwtSettings> jwtSettings,
            IUserRepository userRepository, IMapper mapper)
        {
            _jwtSettings = jwtSettings.Value;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<AuthResponse> Login(LoginDto dto)
        {
            var user = await _userRepository.GetUserAsync(dto.Username);

            if (!CheckUsernameAndPassword(user, dto))
                throw new UnauthorizedException();

            return GenerateToken(user!);
        }

        public async Task RegisterUser(RegisterDto dto)
        {
            if (await _userRepository.UsernameIsExistsAsync(dto.Username))
                throw new UsernameAlreadyExistsException(dto.Username);

            if (await _userRepository.EmailIsExistsAsync(dto.Email))
                throw new EmailAlreadyExistsException(dto.Email);

            var user = _mapper.Map<User>(dto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            user.Role = UserRoles.Admin;

            await _userRepository.CreateUser(user);
        }

        private bool CheckUsernameAndPassword(User? user, LoginDto loginDto)
        {
            if (user == null)
                throw new UnauthorizedException();

            return BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
        }

        private AuthResponse GenerateToken(User user)
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

    }
}