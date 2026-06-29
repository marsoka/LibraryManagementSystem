
namespace Library.DAL.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task<Author?> GetAuthorAsync(int id);
        Task AddAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(int id);
        Task<bool> AuthorIsExistsAsync(int id);

    }
}