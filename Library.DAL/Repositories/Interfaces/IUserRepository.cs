namespace Library.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>?> GetUsersAsync();
        Task<User?> GetUserAsync(string username);
    }
}