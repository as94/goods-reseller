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
        private readonly IProductRepository _productRepository;

        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task<Unit> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAsync(request.ProductId, cancellationToken);
            if (product == null)
            {
                throw new InvalidOperationException($"Product with Id = {request.ProductId} doesn't exist");
            }
            
            product.Update(
                request.Name,
                request.Description,
                new Money(request.UnitPrice),
                new Discount(request.DiscountPerUnit),
                new DateValueObject(DateTime.Now));

            await _productRepository.SaveAsync(product, cancellationToken);
            
            return new Unit();
        }
    }
}