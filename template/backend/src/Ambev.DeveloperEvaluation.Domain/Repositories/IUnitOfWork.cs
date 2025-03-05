namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        ICartRepository Carts { get; }
        ISaleRepository Sales { get; }
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
