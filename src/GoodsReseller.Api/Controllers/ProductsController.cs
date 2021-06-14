using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.Api.Models;
using GoodsReseller.DataCatalogContext.Contracts.Models.Products;
using GoodsReseller.DataCatalogContext.Contracts.Products.BatchByQuery;
using GoodsReseller.DataCatalogContext.Contracts.Products.Create;
using GoodsReseller.DataCatalogContext.Contracts.Products.Delete;
using GoodsReseller.DataCatalogContext.Contracts.Products.GetById;
using GoodsReseller.DataCatalogContext.Contracts.Products.GetByLabel;
using GoodsReseller.DataCatalogContext.Contracts.Products.Update;
using GoodsReseller.DataCatalogContext.Contracts.Products.UpdateProductPhoto;
using GoodsReseller.DataCatalogContext.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GoodsReseller.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProductAsync([FromQuery] string label, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetProductByLabelRequest
            {
                Label = label
            }, cancellationToken);

            if (response.Product == null)
            {
                return NotFound();
            }

            return Ok(response.Product);
        }

        [AllowAnonymous]
        [HttpGet("list")]
        public async Task<IActionResult> GetProductListAsync([FromQuery] BatchProductsQuery query,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new BatchProductsByQueryRequest
            {
                Query = query
            }, cancellationToken);

            return Ok(response.ProductList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(
            [FromBody] [Required] ProductInfoContract product,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new CreateProductRequest
            {
                ProductInfo = product
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
                ProductInfo = product
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

        [HttpPost("{productId}/photos")]
        public async Task UploadProductPhotoAsync(
            [Required] [FromRoute] Guid productId,
            [Required] [FromForm] FileUpload fileUpload,
            CancellationToken cancellationToken)
        {
            var photoPath = Path.Combine(
                _webHostEnvironment.WebRootPath,
                "assets",
                productId.ToString());

            if (!Directory.Exists(photoPath))
            {
                Directory.CreateDirectory(photoPath);
            }

            var fileName = fileUpload.FileName.ToLower();
            var path = Path.Combine(photoPath, fileName);

            await using var fileStream = System.IO.File.Create(path);
            await fileUpload.FileContent.CopyToAsync(fileStream, cancellationToken);

            var relativePath = Path.Combine(productId.ToString(), fileName);
            await _mediator.Send(new UpdateProductPhotoRequest
                {
                    ProductId = productId,
                    PhotoPath = relativePath
                },
                cancellationToken);
        }

        [HttpDelete("{productId}/photos")]
        public async Task RemoveProductPhotoAsync(
            [FromRoute] [Required] Guid productId,
            CancellationToken cancellationToken)
        {
            var photoPath = Path.Combine(
                _webHostEnvironment.WebRootPath,
                "assets",
                productId.ToString());

            if (Directory.Exists(photoPath))
            {
                Directory.Delete(photoPath, true);
            }
            
            await _mediator.Send(new UpdateProductPhotoRequest
                {
                    ProductId = productId,
                    PhotoPath = string.Empty
                },
                cancellationToken);
        }
    }
}