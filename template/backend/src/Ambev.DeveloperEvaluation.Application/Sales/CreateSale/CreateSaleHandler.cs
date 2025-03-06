using Ambev.DeveloperEvaluation.Application.Sales.Shared.Result;
using Ambev.DeveloperEvaluation.Application.Util;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : GenericHandler<CreateSaleCommand, CreateSaleResult, CreateSaleCommandValidator>
    {
        public CreateSaleHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<CreateSaleResult> ExecuteHandlerCode(CreateSaleCommand request, CancellationToken cancelation)
        {
            var sale = _mapper.Map<Sale>(request);

            var result = await _unitOfWork.Sales.CreateAsync(sale, cancelation);
            await _unitOfWork.CommitAsync(cancelation);

            return new CreateSaleResult { Sale = _mapper.Map<SaleResult>(result) };
        }
    }
}
