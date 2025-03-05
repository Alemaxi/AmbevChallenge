using Ambev.DeveloperEvaluation.Application.Carts.Shared.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartCommand : IRequest<UpdateCartResult>
    {
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
        [FromBody]
        public int UserId { get; set; }
        [FromBody]
        public DateTime Date { get; set; }
        [FromBody]
        public ICollection<CartProductCommand> Products { get; set; } = new List<CartProductCommand>();
    }
}
