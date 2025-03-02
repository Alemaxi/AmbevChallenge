using Ambev.DeveloperEvaluation.Application.Util;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using System.Threading;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductHandler : GenericHandler<CreateProductCommand, CreateProductResult, CreateProductCommandValidator>
    {

        public CreateProductHandler(IUnitOfWork unitOfWork, IMapper mapper) :base(unitOfWork, mapper)
        {
        }

        public async override Task<CreateProductResult> ExecuteHandlerCode(CreateProductCommand request, CancellationToken cancelation)
        {
            var product = _mapper.Map<Product>(request);

            var result = await _unitOfWork.Products.CreateAsync(product, cancelation);

            return _mapper.Map<CreateProductResult>(result);
        }
    }
}
