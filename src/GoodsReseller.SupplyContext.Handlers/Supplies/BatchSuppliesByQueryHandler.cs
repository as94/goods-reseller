using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.SupplyContext.Contracts.Supplies.BatchByQuery;
using MediatR;

namespace GoodsReseller.SupplyContext.Handlers.Supplies
{
    public class BatchSuppliesByQueryHandler : IRequestHandler<BatchSuppliesByQueryRequest, BatchSuppliesByQueryResponse>
    {
        public Task<BatchSuppliesByQueryResponse> Handle(BatchSuppliesByQueryRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}