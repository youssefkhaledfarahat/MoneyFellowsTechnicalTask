using MediatR;
using MoneyFellows.Products.Application.Commands;
using MoneyFellows.Products.Application.Exceptions;
using MoneyFellows.Products.Core.Entities;
using MoneyFellows.Products.Core.Interfaces;

namespace MoneyFellows.Products.Application.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            await _productRepository.DeleteAsync(product.Id);

            return Unit.Value;
        }
    }
}
