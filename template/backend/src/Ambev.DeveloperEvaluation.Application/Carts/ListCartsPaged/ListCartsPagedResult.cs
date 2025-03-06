using Ambev.DeveloperEvaluation.Application.Carts.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCartsPaged
{
    public class ListCartsPagedResult
    {
        public int Total { get; set; }
        public List<CartResult> cartsResult { get; set; }
    }
}
