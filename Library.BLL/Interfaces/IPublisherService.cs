using Library.BLL.DTOs.PublisherDTO;

namespace Library.BLL.Interfaces
{
    public interface IPublisherService
    {
        Task<IEnumerable<PublisherDto>> GetPublishersAsync();

        Task<PublisherDto?> GetPublisherAsync(int id);

        Task CreatePublisherAsync(CreatePublisherDto dto);

        Task UpdatePublisherAsync(int id, UpdatePublisherDto dto);

        Task DeletePublisherAsync(int id);
    }
}