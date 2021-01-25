using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.DataCatalogContext.Contracts.Models.Products;
using GoodsReseller.DataCatalogContext.Contracts.Products.BatchByIds;
using GoodsReseller.DataCatalogContext.Handlers.Converters;
using GoodsReseller.DataCatalogContext.Models.Products;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Handlers.Products
{
    public class BatchProductsByIdsHandler : IRequestHandler<BatchProductsByIdsRequest, BatchProductsByIdsResponse>
    {
        private readonly IProductRepository _productRepository;

        public BatchProductsByIdsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task<BatchProductsByIdsResponse> Handle(BatchProductsByIdsRequest request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.BatchAsync(request.Query.Offset, request.Query.Count, cancellationToken);
            
            return new BatchProductsByIdsResponse
            {
                ProductList = new ProductListContract
                {
                    Items = products
                        .Select(x => x.ToListItemContract(x.ProductIds != null && x.ProductIds.Any()))
                        .ToArray()
                }
            };
        }
    }
}