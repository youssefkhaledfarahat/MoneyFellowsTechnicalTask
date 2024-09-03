using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyFellows.Products.Application.Commands;
using MoneyFellows.Products.Application.Queries;

namespace MoneyFellows.Products.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            
            return product != null ? Ok(product) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            var productId = await _mediator.Send(command);
            
            return CreatedAtAction(nameof(GetProductById), new { id = productId }, productId);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await _mediator.Send(command);
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            
            return NoContent();
        }
    }
}
