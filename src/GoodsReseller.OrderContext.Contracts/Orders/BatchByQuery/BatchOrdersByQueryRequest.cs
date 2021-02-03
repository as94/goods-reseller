using GoodsReseller.OrderContext.Contracts.Queries;
using MediatR;

namespace GoodsReseller.OrderContext.Contracts.Orders.BatchByQuery
{
    public class BatchOrdersByQueryRequest : IRequest<BatchOrdersByQueryResponse>
    {
        public BatchOrdersQuery Query { get; set; }
    }
}