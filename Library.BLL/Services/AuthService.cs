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
using Library.Domain.Enums;
using Library.Domain.Responses;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Library.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthService(IOptions<JwtSettings> jwtSettings, IJwtService jwtService,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _jwtSettings = jwtSettings.Value;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AuthResponse> Login(LoginDto dto)
        {
            var user = await _unitOfWork.Users.Find(u => u.Username == dto.Username);

            if (!CheckUsernameAndPassword(user, dto))
                throw new UnauthorizedException();

            return await GenerateToken(user!);
        }

        public async Task RegisterUser(RegisterDto dto)
        {
            if (await _unitOfWork.Users.IsExistsAsync(u => u.Username == dto.Username))
                throw new UsernameAlreadyExistsException(dto.Username);

            if (await _unitOfWork.Users.IsExistsAsync(u => u.Email == dto.Email))
                throw new EmailAlreadyExistsException(dto.Email);

            var user = _mapper.Map<User>(dto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            user.Role = UserRoles.Admin;

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();
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

            await _unitOfWork.RefreshTokens.AddAsync(Refresh);
            await _unitOfWork.CompleteAsync();

            return new AuthResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes)
            };
        }


        public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
        {
            var refreshToken = await _unitOfWork.RefreshTokens
                .Find(rt => rt.Token == refreshTokenRequest.RefreshToken, ["User"]);

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
            refreshToken.RevokedReason = RefreshTokenRevokedReason.Rotation;
            await _unitOfWork.RefreshTokens.UpdateAsync(refreshToken);

            await _unitOfWork.RefreshTokens.AddAsync(
                new RefreshToken
                {
                    Token = newRefreshToken,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    IsRevoked = false,
                    UserId = user.Id
                }
            );

            await _unitOfWork.CompleteAsync();

            return new AuthResponse
            {
                Token = accessToken,
                RefreshToken = newRefreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes)
            };
        }

        public async Task Logout(ClaimsPrincipal user, LogoutRequest logoutRequest)
        {
            var refresh = await _unitOfWork.RefreshTokens
                .Find(rt => rt.Token == logoutRequest.RefreshToken);

            // if(!await _userRepository.UserIsExistsAsync(UserId))
            //     throw new UserNotFoundException(UserId);

            if (refresh == null)
                throw new RefreshTokenInvalidUnauthorizedException();

            if (refresh.ExpiresAt < DateTime.UtcNow)
                throw new RefreshTokenExpiredUnauthorizedException();

            if (refresh.IsRevoked)
                throw new RefreshTokenIsRevokedUnauthorizedException();


            var userId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            if (refresh.UserId != userId)
                throw new UnauthorizedException();


            refresh.IsRevoked = true;

            refresh.RevokedAt = DateTime.UtcNow;

            refresh.RevokedReason = RefreshTokenRevokedReason.Logout;

            await _unitOfWork.RefreshTokens.UpdateAsync(refresh);
            await _unitOfWork.CompleteAsync();

        }


    }
}