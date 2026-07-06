namespace Library.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>?> GetUsersAsync();
        Task<User?> GetUserAsync(string username);
    }
}