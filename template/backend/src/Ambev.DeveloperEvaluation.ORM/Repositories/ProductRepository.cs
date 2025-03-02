using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;



namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {

        public ProductRepository(DefaultContext context): base(context) { }

        public async Task<List<string>> ListCategories()
        {
            return await _context.Products.AsNoTracking().Select(p => p.Category).Distinct().ToListAsync();
        }

        public async Task<List<Product>> ListProductsByCategory(string category, int page, int size, string orderBy)
        {
            var query = _context.Products.AsNoTracking().Where(p => p.Equals(category));

            query = !string.IsNullOrEmpty(orderBy) ? query.OrderBy(orderBy) : query;

            return await query.Skip((page - 1) * size).Take(size).ToListAsync();
        }
    }
}
