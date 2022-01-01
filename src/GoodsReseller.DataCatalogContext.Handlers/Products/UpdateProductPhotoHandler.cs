using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.DataCatalogContext.Contracts.Products.UpdateProductPhoto;
using GoodsReseller.DataCatalogContext.Models.Products;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Handlers.Products
{
    public class UpdateProductPhotoHandler : IRequestHandler<UpdateProductPhotoRequest, Unit>
    {
        private readonly IProductsRepository _productsRepository;

        public UpdateProductPhotoHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        
        public async Task<Unit> Handle(UpdateProductPhotoRequest request, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.GetAsync(request.ProductId, cancellationToken);
            if (product == null)
            {
                throw new InvalidOperationException($"Product with Id = {request.ProductId} doesn't exist");
            }
            
            product.UpdateProductPhoto(request.Version, request.PhotoPath);

            await _productsRepository.SaveAsync(product, cancellationToken);
            
            return Unit.Value;
        }
    }
}