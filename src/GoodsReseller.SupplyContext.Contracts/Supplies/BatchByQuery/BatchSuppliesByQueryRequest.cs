using GoodsReseller.SupplyContext.Contracts.Queries;
using MediatR;

namespace GoodsReseller.SupplyContext.Contracts.Supplies.BatchByQuery
{
    public class BatchSuppliesByQueryRequest : IRequest<BatchSuppliesByQueryResponse>
    {
        public BatchSuppliesQuery Query { get; set; }
    }
}