
public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAuthorsAsync();

    Task<AuthorDto?> GetAuthorAsync(int id);

    Task CreateAuthorAsync(CreateAuthorDto dto);

    Task UpdateAuthorAsync(int id, UpdateAuthorDto dto);

    Task DeleteAuthorAsync(int id);
}