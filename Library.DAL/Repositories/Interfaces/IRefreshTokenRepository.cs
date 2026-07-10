namespace Library.DAL.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<IEnumerable<RefreshToken>> GetRefreshTokensAsync();
        Task<RefreshToken?> GetRefreshTokenAsync(int id);
        Task<RefreshToken?> GetRefreshTokenAsync(string token);
        Task AddRefreshTokenAsync(RefreshToken refreshToken);
        Task DeleteRefreshTokenAsync(int id);
    }
}