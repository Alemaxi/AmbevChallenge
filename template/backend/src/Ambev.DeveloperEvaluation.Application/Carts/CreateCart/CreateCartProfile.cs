using Ambev.DeveloperEvaluation.Application.Carts.Shared.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartProfile : Profile
    {
        public CreateCartProfile()
        {
            CreateMap<CreateCartCommand, Cart>();
            CreateMap<CartProductCommand, CartProduct>();
        }
    }
}
