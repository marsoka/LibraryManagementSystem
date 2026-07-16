using System.Linq.Expressions;
using System.Xml.Linq;
using Library.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>?> GetAllAsync(string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (string include in includes)
                    query = query.Include(include);

            return await query.ToListAsync();
        }

        public async Task<T?> Find(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<T?> Find(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return await query.FirstOrDefaultAsync(expression);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> IsExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }

    }
}