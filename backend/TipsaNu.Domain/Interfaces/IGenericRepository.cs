using System.Linq.Expressions;
using System.Threading;

namespace TipsaNu.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    }
}
