using Library.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL.Repositories.Implementations
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly ApplicationDbContext _context;
        public PublisherRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddPublisherAsync(Publisher publisher)
        {
            await _context.Publishers.AddAsync(publisher);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePublisherAsync(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Publisher?> GetPublisherAsync(int id)
        {
            return await _context.Publishers.FindAsync(id);
        }

        public async Task<IEnumerable<Publisher>> GetPublishersAsync()
        {
            return await _context.Publishers.ToListAsync();
        }

        public async Task<bool> PublisherIsExistsAsync(int id)
        {
            return await _context.Publishers.AnyAsync(p => p.Id == id);
        }

        public async Task UpdatePublisherAsync(Publisher publisher)
        {
            _context.Publishers.Update(publisher);
            await _context.SaveChangesAsync();
        }
    }
}