using Library.BLL.Exceptions.NotFoundExceptions;
using Library.BLL.Interfaces;
using Library.DAL.Repositories.Interfaces;

namespace Library.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<User?> GetUserAsync(string username)
        {
            return await _repo.GetUserAsync(username);
        }

        public Task<IEnumerable<User>?> GetUsersAsync()
        {
            throw new NotImplementedException();
        }
    }
}