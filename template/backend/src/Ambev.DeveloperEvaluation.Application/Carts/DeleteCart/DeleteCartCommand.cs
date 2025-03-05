
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
    public class DeleteCartCommand : IRequest<string>
    {
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
    }
}
