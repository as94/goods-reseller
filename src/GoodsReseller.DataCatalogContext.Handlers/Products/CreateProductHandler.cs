using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.DataCatalogContext.Contracts.Products.Create;
using GoodsReseller.DataCatalogContext.Models.Products;
using GoodsReseller.SeedWork.ValueObjects;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Handlers.Products
{
    public class CreateProductHandler : IRequestHandler<CreateProductRequest>
    {
        private readonly IProductsRepository _productsRepository;

        public CreateProductHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        
        public async Task<Unit> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var product = new Product(
                request.ProductInfo.Id,
                request.ProductInfo.Version,
                request.ProductInfo.Label,
                request.ProductInfo.Name,
                request.ProductInfo.Description,
                new Money(request.ProductInfo.UnitPrice),
                new Discount(request.ProductInfo.DiscountPerUnit),
                new Money(request.ProductInfo.AddedCost),
                request.ProductInfo.ProductIds);

            await _productsRepository.SaveAsync(product, cancellationToken);
            
            return Unit.Value;
        }
    }
}