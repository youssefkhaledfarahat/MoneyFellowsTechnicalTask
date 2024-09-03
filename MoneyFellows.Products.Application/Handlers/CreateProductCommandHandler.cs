using MediatR;
using MoneyFellows.Products.Application.Commands;
using MoneyFellows.Products.Core.Interfaces;
using MoneyFellows.Products.Core.Entities;

namespace MoneyFellows.Products.Application.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _repository;

        public CreateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                StockQuantity = request.StockQuantity
            };

            await _repository.AddAsync(product);

            return product.Id;
        }
    }
}
