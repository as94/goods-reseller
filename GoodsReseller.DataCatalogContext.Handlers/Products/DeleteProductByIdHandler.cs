using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.DataCatalogContext.Contracts.Products.Delete;
using GoodsReseller.DataCatalogContext.Models.Products;
using GoodsReseller.SeedWork.ValueObjects;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Handlers.Products
{
    public class DeleteProductByIdHandler : IRequestHandler<DeleteProductByIdRequest, Unit>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task<Unit> Handle(DeleteProductByIdRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAsync(request.ProductId, cancellationToken);
            if (product == null)
            {
                throw new InvalidOperationException($"Product with Id = {request.ProductId} doesn't exist");
            }
            
            product.Remove(new DateValueObject(DateTime.Now));

            await _productRepository.SaveAsync(product, cancellationToken);
            
            return new Unit();
        }
    }
}