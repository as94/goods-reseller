using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.DataCatalogContext.Contracts.Models;
using GoodsReseller.DataCatalogContext.Contracts.Products.Create;
using GoodsReseller.DataCatalogContext.Contracts.Products.Delete;
using GoodsReseller.DataCatalogContext.Contracts.Products.GetById;
using GoodsReseller.DataCatalogContext.Contracts.Products.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodsReseller.Api.Controllers
{
    [Authorize(Roles = "Admin,Customer")]
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductAsync(Guid productId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetProductByIdRequest
            {
                ProductId = productId
            }, cancellationToken);

            if (response.Product == null)
            {
                return NotFound();
            }

            return Ok(response.Product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(
            [FromBody] [Required] ProductInfoContract product,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new CreateProductRequest
            {
                Name = product.Name,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                DiscountPerUnit = product.DiscountPerUnit
            }, cancellationToken);

            return Ok(response);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProductAsync(
            [FromRoute] [Required] Guid productId,
            [FromBody] [Required] ProductInfoContract product,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateProductRequest
            {
                ProductId = productId,
                
                Name = product.Name,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                DiscountPerUnit = product.DiscountPerUnit
            }, cancellationToken);
            
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductAsync(
            [FromRoute] [Required] Guid productId,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteProductByIdRequest
            {
                ProductId = productId
            }, cancellationToken);
            
            return Ok();
        }
    }
}