using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.DataCatalogContext.Contracts.Products.Delete;
using GoodsReseller.DataCatalogContext.Models.Products;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Handlers.Products
{
    public class DeleteProductByIdHandler : IRequestHandler<DeleteProductByIdRequest, Unit>
    {
        private readonly IProductsRepository _productsRepository;

        public DeleteProductByIdHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        
        public async Task<Unit> Handle(DeleteProductByIdRequest request, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.GetAsync(request.ProductId, cancellationToken);
            if (product == null)
            {
                throw new InvalidOperationException($"Product with Id = {request.ProductId} doesn't exist");
            }
            
            product.Remove();

            await _productsRepository.SaveAsync(product, cancellationToken);
            
            return new Unit();
        }
    }
}