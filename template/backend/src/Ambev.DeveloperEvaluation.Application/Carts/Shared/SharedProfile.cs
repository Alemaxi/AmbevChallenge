using Ambev.DeveloperEvaluation.Application.Carts.Shared.CartResult;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.Shared
{
    public class SharedProfile : Profile
    {
        public SharedProfile()
        {
            CreateMap<Cart, CartProductResult>();
            CreateMap<CartProduct, CartProductResult>();
        }
    }
}
