namespace Library.BLL.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<IEnumerable<RefreshToken>> GetRefreshTokensAsync();
        Task<RefreshToken> GetRefreshTokenAsync(int id);
        Task CreateRefreshToken(RefreshToken refreshToken);
        Task DeleteRefreshToken(int id);
    }
}