using System.Linq.Expressions;

namespace Library.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>?> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByExpressionAsync(Expression<Func<T, bool>> expression, string[] includes = null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> IsExistsAsync(Expression<Func<T, bool>> expression);
    }
}