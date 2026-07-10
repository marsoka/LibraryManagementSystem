namespace Library.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>?> GetUsersAsync();
        Task<User?> GetUserAsync(string username);
        Task CreateUser(User user);
        Task<bool> UserIsExistsAsync(int id);
        Task<bool> UsernameIsExistsAsync(string username);
        Task<bool> EmailIsExistsAsync(string email);
    }
}