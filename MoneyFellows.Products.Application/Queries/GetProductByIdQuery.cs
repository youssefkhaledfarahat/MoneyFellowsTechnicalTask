﻿using MediatR;
using MoneyFellows.Products.Application.DTOs;

namespace MoneyFellows.Products.Application.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<ProductDto>;
}
