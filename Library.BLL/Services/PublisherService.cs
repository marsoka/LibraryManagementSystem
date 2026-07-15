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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PublisherService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreatePublisherAsync(CreatePublisherDto dto)
        {
            var publisher = _mapper.Map<Publisher>(dto);
            await _unitOfWork.Publishers.AddAsync(publisher);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeletePublisherAsync(int id)
        {
            var publisher = await _unitOfWork.Publishers.GetByIdAsync(id);
            if (publisher == null)
            {
                throw new PublisherNotFoundException(id);
            }
            await _unitOfWork.Publishers.DeleteAsync(publisher);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<PublisherDto?> GetPublisherAsync(int id)
        {
            var publisher = await _unitOfWork.Publishers.GetByIdAsync(id);
            if (publisher == null)
            {
                throw new PublisherNotFoundException(id);
            }
            return _mapper.Map<PublisherDto>(publisher);
        }

        public async Task<IEnumerable<PublisherDto>> GetPublishersAsync()
        {
            var listOfPublishers = await _unitOfWork.Publishers.GetAllAsync();
            return _mapper.Map<IEnumerable<PublisherDto>>(listOfPublishers);
        }

        public async Task UpdatePublisherAsync(int id, UpdatePublisherDto dto)
        {
            var publisher = await _unitOfWork.Publishers.GetByIdAsync(id);
            if (publisher == null)
            {
                throw new PublisherNotFoundException(id);
            }

            _mapper.Map(dto, publisher);
            await _unitOfWork.Publishers.UpdateAsync(publisher);
            await _unitOfWork.CompleteAsync();
        }
    }
}