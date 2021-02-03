using GoodsReseller.DataCatalogContext.Contracts.Queries;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Contracts.Products.BatchByQuery
{
    public class BatchProductsByQueryRequest : IRequest<BatchProductsByQueryResponse>
    {
        public BatchProductsQuery Query { get; set; }
    }
}