using Ambev.DeveloperEvaluation.Application.Carts.Shared.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartCommand : IRequest<UpdateCartResult>
    {
        public Guid Id { get; private set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public ICollection<CartProductCommand> Products { get; set; } = new List<CartProductCommand>();


        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
