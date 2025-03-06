using Ambev.DeveloperEvaluation.Domain.Entities;
using System.ComponentModel;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository : IGenericRepository<Sale>
    {
        public Task<List<Sale>> GetSalesPagedWithProduct(int page, int size, string order);
        public Task<Sale> GetSaleWithProduct(Guid id);
    }
}
