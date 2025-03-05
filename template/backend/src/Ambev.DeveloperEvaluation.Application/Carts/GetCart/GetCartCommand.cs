using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    public class GetCartCommand : IRequest<GetCartResult>
    {
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
    }
}
