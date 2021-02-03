using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.DataCatalogContext.Contracts.Models.Products;
using GoodsReseller.DataCatalogContext.Contracts.Products.BatchByQuery;
using GoodsReseller.DataCatalogContext.Handlers.Converters;
using GoodsReseller.DataCatalogContext.Models.Products;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Handlers.Products
{
    public class BatchProductsByQueryHandler : IRequestHandler<BatchProductsByQueryRequest, BatchProductsByQueryResponse>
    {
        private readonly IProductRepository _productRepository;

        public BatchProductsByQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task<BatchProductsByQueryResponse> Handle(BatchProductsByQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.BatchAsync(request.Query.Offset, request.Query.Count, cancellationToken);

            return new BatchProductsByQueryResponse
            {
                ProductList = new ProductListContract
                {
                    Items = products
                        .OrderByDescending(x => x.LastUpdateDate ?? x.CreationDate)
                        .Select(x => x.ToListItemContract(x.ProductIds != null && x.ProductIds.Any()))
                        .ToArray()
                }
            };
        }
    }
}