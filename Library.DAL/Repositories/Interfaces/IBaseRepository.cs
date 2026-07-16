using System.Linq.Expressions;

namespace Library.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>?> GetAllAsync(string[] includes = null);
        Task<T?> GetByIdAsync(int id);
        Task<T?> Find(Expression<Func<T, bool>> expression);
        Task<T?> Find(Expression<Func<T, bool>> expression, string[] includes = null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> IsExistsAsync(Expression<Func<T, bool>> expression);
    }
}