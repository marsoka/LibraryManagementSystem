using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Library.BLL.DTOs;
using Library.BLL.DTOs.AuthDTO;
using Library.BLL.Exceptions.AlreadyAlreadyExistsException;
using Library.BLL.Exceptions.BusinessRuleExceptions;
using Library.BLL.Exceptions.NotFoundExceptions;
using Library.BLL.Exceptions.StatusCodesExeptions;
using Library.BLL.Exceptions.UnauthorizedExceptions;
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
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthService(IOptions<JwtSettings> jwtSettings, IJwtService jwtService,
            IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository, IMapper mapper)
        {
            _jwtSettings = jwtSettings.Value;
            _jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<AuthResponse> Login(LoginDto dto)
        {
            var user = await _userRepository.GetUserAsync(dto.Username);

            if (!CheckUsernameAndPassword(user, dto))
                throw new UnauthorizedException();

            return await GenerateToken(user!);
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

        private async Task<AuthResponse> GenerateToken(User user)
        {
            string accessToken = _jwtService.GenerateAccessToken(user);
            string refreshToken = _jwtService.GenerateRefreshToken();

            var Refresh = new RefreshToken
            {
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
                IsRevoked = false,
                UserId = user.Id
            };

            await _refreshTokenRepository.AddRefreshTokenAsync(Refresh);

            return new AuthResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes)
            };
        }


        public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
        {
            var refreshToken = await _refreshTokenRepository
                .GetRefreshTokenAsync(refreshTokenRequest.RefreshToken);

            if (refreshToken == null)
                throw new RefreshTokenInvalidUnauthorizedException();

            if (refreshToken.ExpiresAt < DateTime.UtcNow)
                throw new RefreshTokenExpiredUnauthorizedException();

            if (refreshToken.IsRevoked)
                throw new RefreshTokenIsRevokedUnauthorizedException();

            var user = refreshToken.User;

            var accessToken = _jwtService.GenerateAccessToken(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            // await _refreshTokenRepository.DeleteRefreshTokenAsync(refreshToken.Id);
            refreshToken.IsRevoked = true;
            refreshToken.RevokedAt = DateTime.UtcNow;
            await _refreshTokenRepository.UpdateRefreshTokenAsync(refreshToken);

            await _refreshTokenRepository.AddRefreshTokenAsync(
                new RefreshToken
                {
                    Token = newRefreshToken,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    IsRevoked = false,
                    UserId = user.Id
                }
            );

            return new AuthResponse
            {
                Token = accessToken,
                RefreshToken = newRefreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes)
            };
        }


    }
}