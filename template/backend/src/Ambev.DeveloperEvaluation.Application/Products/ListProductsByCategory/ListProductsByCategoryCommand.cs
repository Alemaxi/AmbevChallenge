using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory
{
    public class ListProductsByCategoryCommand : IRequest<ListProductsByCategoryResult>
    {
        [FromRoute(Name = "category")]
        public string Category { get; set; }
        [FromQuery(Name = "_page")]
        public int Page { get; set; } = 1;
        [FromQuery(Name = "_size")]
        public int Size { get; set; } = 10;
        [FromQuery(Name = "_order")]
        public string Order { get; set; } = string.Empty;
    }
}
