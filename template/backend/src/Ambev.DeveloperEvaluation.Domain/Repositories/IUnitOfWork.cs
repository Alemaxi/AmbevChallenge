namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
