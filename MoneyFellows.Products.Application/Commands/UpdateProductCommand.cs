using MediatR;

namespace MoneyFellows.Products.Application.Commands
{
    public record UpdateProductCommand(int Id, string Name, string Description, decimal Price, int StockQuantity) : IRequest<Unit>;
}
