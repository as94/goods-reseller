using GoodsReseller.DataCatalogContext.Contracts.Queries;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Contracts.Products.BatchByIds
{
    public class BatchProductsByIdsRequest : IRequest<BatchProductsByIdsResponse>
    {
        public BatchProductsQuery Query { get; set; }
    }
}