using Ambev.DeveloperEvaluation.Application.Carts.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.Shared
{
    public class SharedProfile : Profile
    {
        public SharedProfile()
        {
            CreateMap<Cart, CartResult>();
            CreateMap<CartProduct, CartProductResult>();
        }
    }
}
