using AutoMapper;
using Library.BLL.DTOs.CategoryDTO;
using Library.BLL.DTOs.PublisherDTO;
using Library.BLL.Exceptions.NotFoundExceptions;
using Library.BLL.Interfaces;
using Library.DAL.Repositories.Interfaces;

namespace Library.BLL.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _repo;
        private readonly IMapper _mapper;

        public PublisherService(IPublisherRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task CreatePublisherAsync(CreatePublisherDto dto)
        {
            var publisher = _mapper.Map<Publisher>(dto);
            await _repo.AddPublisherAsync(publisher);
        }

        public async Task DeletePublisherAsync(int id)
        {
            if (!await _repo.PublisherIsExistsAsync(id))
            {
                throw new PublisherNotFoundException(id);
            }
            await _repo.DeletePublisherAsync(id);
        }

        public async Task<PublisherDto?> GetPublisherAsync(int id)
        {
            var publisher = await _repo.GetPublisherAsync(id);
            if (publisher == null)
            {
                throw new PublisherNotFoundException(id);
            }
            return _mapper.Map<PublisherDto>(publisher);
        }

        public async Task<IEnumerable<PublisherDto>> GetPublishersAsync()
        {
            var listOfPublishers = await _repo.GetPublishersAsync();
            return _mapper.Map<IEnumerable<PublisherDto>>(listOfPublishers);
        }

        public async Task UpdatePublisherAsync(int id, UpdatePublisherDto dto)
        {
            var publisher = await _repo.GetPublisherAsync(id);
            if (publisher == null)
            {
                throw new PublisherNotFoundException(id);
            }

            _mapper.Map(dto, publisher);
            await _repo.UpdatePublisherAsync(publisher);
        }
    }
}