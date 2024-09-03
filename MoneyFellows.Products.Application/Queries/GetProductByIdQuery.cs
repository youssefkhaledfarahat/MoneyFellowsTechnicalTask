using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MoneyFellows.Products.Application.DTOs;

namespace MoneyFellows.Products.Application.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<ProductDto>;
}
