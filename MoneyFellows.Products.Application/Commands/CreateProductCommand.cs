using MediatR;

namespace MoneyFellows.Products.Application.Commands
{
    public record CreateProductCommand(string Name, string Description, decimal Price, int StockQuantity) : IRequest<int>;
}
