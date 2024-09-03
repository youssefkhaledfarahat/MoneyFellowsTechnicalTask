using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MoneyFellows.Products.Application.DTOs;
using MoneyFellows.Products.Application.Exceptions;
using MoneyFellows.Products.Application.Queries;
using MoneyFellows.Products.Core.Entities;
using MoneyFellows.Products.Core.Interfaces;

namespace MoneyFellows.Products.Application.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);

            if (product == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            return new ProductDto(product.Id, product.Name, product.Description, product.Price, product.StockQuantity);
        }
    }

}
