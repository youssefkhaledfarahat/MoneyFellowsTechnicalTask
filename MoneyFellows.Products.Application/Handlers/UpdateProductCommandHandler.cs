using MediatR;
using MoneyFellows.Products.Application.Commands;
using MoneyFellows.Products.Application.Exceptions;
using MoneyFellows.Products.Core.Entities;
using MoneyFellows.Products.Core.Interfaces;

namespace MoneyFellows.Products.Application.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;

            await _productRepository.UpdateAsync(product);

            return Unit.Value;
        }

    }
}
