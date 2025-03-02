using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<string>> ListCategories();

        Task<List<Product>> ListProductsByCategory(string category, int page, int size, string order);
    }
}
