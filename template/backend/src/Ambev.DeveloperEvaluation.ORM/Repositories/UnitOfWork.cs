using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DefaultContext _context;
        private IProductRepository _productRepository;
        private ICartRepository _cartRepository;
        private ISaleRepository _saleRepository;

        public UnitOfWork(DefaultContext context)
        {
            _context = context;
        }

        public IProductRepository Products => _productRepository ??= new ProductRepository(_context);
        public ICartRepository Carts => _cartRepository ??= new CartRepository(_context);
        public ISaleRepository Sales => _saleRepository ??= new SaleRepository(_context);

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
