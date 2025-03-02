using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllPaginatedAsync(int page, int size, string order, CancellationToken cancellationToken = default);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
