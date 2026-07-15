
using AutoMapper;
using Library.BLL.Exceptions.NotFoundExceptions;
using Library.BLL.Interfaces;
using Library.DAL.Repositories.Interfaces;

public class AuthorService : IAuthorService
{
    // replece IAuthorRepository by IBaseRepository
    // private readonly IAuthorRepository _repo;

    // private readonly IBaseRepository<Author> _repo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task CreateAuthorAsync(CreateAuthorDto dto)
    {
        var author = _mapper.Map<Author>(dto);

        await _unitOfWork.Authors.AddAsync(author);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteAuthorAsync(int id)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(id);

        // if (!await _repo.IsExistsAsync(a => a.Id == id))
        if (author == null)
        {
            throw new AuthorNotFoundException(id);
        }

        await _unitOfWork.Authors.DeleteAsync(author);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<AuthorDto?> GetAuthorAsync(int id)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(id);

        if (author == null)
        {
            throw new AuthorNotFoundException(id);
        }

        var dto = _mapper.Map<AuthorDto>(author);
        return dto;
    }

    public async Task<IEnumerable<AuthorDto>> GetAuthorsAsync()
    {
        var listOfAuthor = await _unitOfWork.Authors.GetAllAsync();
        return _mapper.Map<IEnumerable<AuthorDto>>(listOfAuthor);
    }

    public async Task UpdateAuthorAsync(int id, UpdateAuthorDto dto)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(id);

        if (author == null)
        {
            throw new AuthorNotFoundException(id);
        }

        _mapper.Map(dto, author);

        await _unitOfWork.Authors.UpdateAsync(author);
        await _unitOfWork.CompleteAsync();
    }
}