using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IProductRepository
    {
        /// <summary>
        /// Cria um novo produto no repositório.
        /// </summary>
        /// <param name="product">O produto a ser criado.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>O produto criado.</returns>
        Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);

        /// <summary>
        /// Recupera um produto pelo seu identificador único.
        /// </summary>
        /// <param name="id">O identificador único do produto.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>O produto, se encontrado; caso contrário, null.</returns>
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Recupera um produto pelo seu nome.
        /// </summary>
        /// <param name="name">O nome do produto.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>O produto, se encontrado; caso contrário, null.</returns>
        Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Atualiza um produto existente no repositório.
        /// </summary>
        /// <param name="product">O produto a ser atualizado.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>O produto atualizado.</returns>
        Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default);

        /// <summary>
        /// Exclui um produto do repositório.
        /// </summary>
        /// <param name="id">O identificador único do produto a ser excluído.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>True se o produto foi excluído; false caso não tenha sido encontrado.</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
