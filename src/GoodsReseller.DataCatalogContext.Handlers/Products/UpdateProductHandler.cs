using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.DataCatalogContext.Contracts.Products.Update;
using GoodsReseller.DataCatalogContext.Models.Products;
using GoodsReseller.SeedWork.ValueObjects;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Handlers.Products
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, Unit>
    {
        private readonly IProductsRepository _productsRepository;

        public UpdateProductHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        
        public async Task<Unit> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.GetAsync(request.ProductId, cancellationToken);
            if (product == null)
            {
                throw new InvalidOperationException($"Product with Id = {request.ProductId} doesn't exist");
            }
            
            product.Update(
                request.ProductInfo.Label,
                request.ProductInfo.Name,
                request.ProductInfo.Description,
                new Money(request.ProductInfo.UnitPrice),
                new Discount(request.ProductInfo.DiscountPerUnit),
                new DateValueObject(),
                request.ProductInfo.ProductIds);

            await _productsRepository.SaveAsync(product, cancellationToken);
            
            return new Unit();
        }
    }
}