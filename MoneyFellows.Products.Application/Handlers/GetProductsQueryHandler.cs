using MediatR;
using MoneyFellows.Products.Application.DTOs;
using MoneyFellows.Products.Application.Queries;
using MoneyFellows.Products.Core.Interfaces;

namespace MoneyFellows.Products.Application.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _repository;

        public GetProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync();
            return products.Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity));
        }
    }

}
