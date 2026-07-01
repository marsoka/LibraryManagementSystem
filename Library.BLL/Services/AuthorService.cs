
using AutoMapper;
using Library.BLL.Exceptions.NotFoundExceptions;
using Library.BLL.Interfaces;
using Library.DAL.Repositories.Interfaces;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repo;
    private readonly IMapper _mapper;

    public AuthorService(IAuthorRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task CreateAuthorAsync(CreateAuthorDto dto)
    {
        var author = _mapper.Map<Author>(dto);

        await _repo.AddAuthorAsync(author);
    }

    public async Task DeleteAuthorAsync(int id)
    {
        if (!await _repo.AuthorIsExistsAsync(id))
        {
            throw new AuthorNotFoundException(id);
        }

        await _repo.DeleteAuthorAsync(id);
    }

    public async Task<AuthorDto?> GetAuthorAsync(int id)
    {
        var author = await _repo.GetAuthorAsync(id);

        if (author == null)
        {
            throw new AuthorNotFoundException(id);
        }

        var dto = _mapper.Map<AuthorDto>(author);
        return dto;
    }

    public async Task<IEnumerable<AuthorDto>> GetAuthorsAsync()
    {
        var listOfAuthor = await _repo.GetAuthorsAsync();
        return _mapper.Map<IEnumerable<AuthorDto>>(listOfAuthor);
    }

    public async Task UpdateAuthorAsync(int id, UpdateAuthorDto dto)
    {
        var author = await _repo.GetAuthorAsync(id);

        if (author == null)
        {
            throw new AuthorNotFoundException(id);
        }

        _mapper.Map(dto, author);

        await _repo.UpdateAuthorAsync(author);

    }
}