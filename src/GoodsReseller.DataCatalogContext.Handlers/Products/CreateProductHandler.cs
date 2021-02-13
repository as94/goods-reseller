using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.DataCatalogContext.Contracts.Products.Create;
using GoodsReseller.DataCatalogContext.Models.Products;
using GoodsReseller.SeedWork.ValueObjects;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Handlers.Products
{
    public class CreateProductHandler : IRequestHandler<CreateProductRequest, CreateProductResponse>
    {
        private readonly IProductsRepository _productsRepository;

        public CreateProductHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        
        public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var productId = Guid.NewGuid();
            var version = 1;
            
            var product = new Product(
                productId,
                version,
                request.ProductInfo.Label,
                request.ProductInfo.Name,
                request.ProductInfo.Description,
                new Money(request.ProductInfo.UnitPrice),
                new Discount(request.ProductInfo.DiscountPerUnit),
                request.ProductInfo.ProductIds);

            await _productsRepository.SaveAsync(product, cancellationToken);
            
            return new CreateProductResponse
            {
                ProductId = productId
            };
        }
    }
}