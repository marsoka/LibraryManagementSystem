namespace Library.DAL.Repositories.Interfaces
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<Publisher>> GetPublishersAsync();
        Task<Publisher?> GetPublisherAsync(int id);
        Task AddPublisherAsync(Publisher publisher);
        Task UpdatePublisherAsync(Publisher publisher);
        Task DeletePublisherAsync(int id);
        Task<bool> PublisherIsExistsAsync(int id);
    }
}