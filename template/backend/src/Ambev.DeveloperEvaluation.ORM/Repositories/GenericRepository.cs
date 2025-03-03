using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
{
    protected readonly DefaultContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(DefaultContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        return entity;
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllPaginatedAsync(int page, int size, string order, CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _dbSet;

        // Ordena pelo Id. Você pode melhorar esse mecanismo para ordenar por outras propriedades
        query = !string.IsNullOrEmpty(order) ? query.OrderBy(order) : query;

        return await query.Skip((page - 1) * size).Take(size).ToListAsync(cancellationToken);
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        return true;
    }
}