using Library.BLL.Exceptions.NotFoundExceptions;
using Library.BLL.Exceptions.UnauthorizedExceptions;
using Library.BLL.Interfaces;
using Library.DAL.Repositories.Interfaces;

namespace Library.BLL.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _repo;

        public RefreshTokenService(IRefreshTokenRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateRefreshToken(RefreshToken refreshToken)
        {
            await _repo.AddRefreshTokenAsync(refreshToken);
        }

        public async Task DeleteRefreshToken(int id)
        {
            var RefreshToken = await _repo.GetRefreshTokenAsync(id);
            if (RefreshToken == null)
                throw new RefreshTokenInvalidUnauthorizedException(id);

            await _repo.DeleteRefreshTokenAsync(id);
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(int id)
        {
            var refreshToken = await _repo.GetRefreshTokenAsync(id);
            if (refreshToken == null)
                throw new RefreshTokenInvalidUnauthorizedException(id);

            return refreshToken;
        }

        public async Task<IEnumerable<RefreshToken>> GetRefreshTokensAsync()
        {
            return await _repo.GetRefreshTokensAsync();
        }
    }
}