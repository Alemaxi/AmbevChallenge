using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListCategories
{
    public class ListCategoriesCommand : IRequest<List<string>>
    {
    }
}
