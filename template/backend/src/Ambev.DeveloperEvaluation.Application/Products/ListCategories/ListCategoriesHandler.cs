using Ambev.DeveloperEvaluation.Application.Util;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.ListCategories
{
    public class ListCategoriesHandler : GenericHandler<ListCategoriesCommand, List<string>, ListCategoriesCommandValidator>
    {
        public ListCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<List<string>> ExecuteHandlerCode(ListCategoriesCommand request, CancellationToken cancelation)
        {
            return await _unitOfWork.Products.ListCategories();
        }
    }
}
