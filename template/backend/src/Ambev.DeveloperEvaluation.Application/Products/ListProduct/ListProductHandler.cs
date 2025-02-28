using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct
{
    public class ListProductHandler : IRequestHandler<ListProductCommand, ListProductResult>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ListProductHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ListProductResult> Handle(ListProductCommand command, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllPaginated(command.Page, command.Size, command.Order, cancellationToken);
            return new ListProductResult
            {
                Count = result.Count(),
                Page = command.Page,
                Size = command.Size,
                Produtos = result.Select(product => _mapper.Map<ProductResult>(product)).ToList()
            };
        }
    }
}
