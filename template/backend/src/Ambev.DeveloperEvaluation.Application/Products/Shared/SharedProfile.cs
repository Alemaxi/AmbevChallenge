using Ambev.DeveloperEvaluation.Application.Products.Shared.ProductRatingCommandShared;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.Shared
{
    public class SharedProfile : Profile
    {
        public SharedProfile()
        {
            CreateMap<ProductRatingCommand, ProductRating>();
            CreateMap<ProductRating, ProductRatingResult>();
        }
    }
}
