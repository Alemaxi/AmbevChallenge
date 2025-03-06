using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        public SaleRepository(DefaultContext context) : base(context)
        {
        }


        public async Task<List<Sale>> GetSalesPagedWithProduct(int page, int size, string order)
        {
            var query = _context.Sales.Include(x => x.Items).ThenInclude(i => i.Product).AsQueryable();

            // Ordena pelo Id. Você pode melhorar esse mecanismo para ordenar por outras propriedades
            query = !string.IsNullOrEmpty(order) ? query.OrderBy(order) : query;

            return await query.Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public async Task<Sale> GetSaleWithProduct(Guid id)
        {
            return await _context.Sales.Include(x => x.Items).ThenInclude(i => i.Product).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
