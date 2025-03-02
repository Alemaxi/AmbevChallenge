
using Ambev.DeveloperEvaluation.Application.Util;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using System.Runtime.InteropServices;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductHandler : GenericHandler<UpdateProductCommand, UpdateProductResult, UpdateProductCommandValidator>
    {

        public UpdateProductHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<UpdateProductResult> ExecuteHandlerCode(UpdateProductCommand Request, CancellationToken cancellation)
        {
            var entity = _mapper.Map<Product>(Request);

            var result = await _unitOfWork.Products.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<UpdateProductResult>(result);
        }
    }
}
