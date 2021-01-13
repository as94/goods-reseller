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
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var productId = Guid.NewGuid();
            var version = 1;
            
            var product = new Product(
                productId,
                version,
                request.Name,
                request.Description,
                new Money(request.UnitPrice),
                new Discount(request.DiscountPerUnit),
                new DateValueObject(DateTime.Now));

            await _productRepository.SaveAsync(product, cancellationToken);
            
            return new CreateProductResponse
            {
                ProductId = productId
            };
        }
    }
}