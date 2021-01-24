using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.DataCatalogContext.Contracts.Products.GetByLabel;
using GoodsReseller.DataCatalogContext.Handlers.Converters;
using GoodsReseller.DataCatalogContext.Models.Products;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Handlers.Products
{
    public class GetProductByLabelHandler : IRequestHandler<GetProductByLabelRequest, GetProductByLabelResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByLabelHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task<GetProductByLabelResponse> Handle(GetProductByLabelRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Label))
            {
                return new GetProductByLabelResponse();
            }
            
            var product = await _productRepository.GetAsync(request.Label, cancellationToken);
            
            if (product == null)
            {
                return new GetProductByLabelResponse();
            }

            IEnumerable<Product> innerProducts = null;
            if (product.ProductIds?.Length > 0)
            {
                innerProducts = await _productRepository.GetListByIdsAsync(product.ProductIds, cancellationToken);
            }

            return new GetProductByLabelResponse
            {
                Product = product.ToContract(innerProducts)
            };
        }
    }
}