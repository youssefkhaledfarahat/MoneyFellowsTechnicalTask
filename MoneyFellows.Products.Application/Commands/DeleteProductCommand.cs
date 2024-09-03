using MediatR;

namespace MoneyFellows.Products.Application.Commands
{
    public record DeleteProductCommand(int Id) : IRequest<Unit>;
}
