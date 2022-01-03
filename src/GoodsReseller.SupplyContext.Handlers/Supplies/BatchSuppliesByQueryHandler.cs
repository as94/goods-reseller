using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.SupplyContext.Contracts.Models;
using GoodsReseller.SupplyContext.Contracts.Supplies.BatchByQuery;
using GoodsReseller.SupplyContext.Domain.Supplies;
using GoodsReseller.SupplyContext.Handlers.Converters;
using MediatR;

namespace GoodsReseller.SupplyContext.Handlers.Supplies
{
    public class BatchSuppliesByQueryHandler : IRequestHandler<BatchSuppliesByQueryRequest, BatchSuppliesByQueryResponse>
    {
        private readonly ISuppliesRepository _suppliesRepository;

        public BatchSuppliesByQueryHandler(ISuppliesRepository suppliesRepository)
        {
            _suppliesRepository = suppliesRepository;
        }
        
        public async Task<BatchSuppliesByQueryResponse> Handle(BatchSuppliesByQueryRequest request, CancellationToken cancellationToken)
        {
            var (supplies, rowsCount) = await _suppliesRepository.BatchAsync(request.Query.Offset, request.Query.Count, cancellationToken);

            return new BatchSuppliesByQueryResponse
            {
                SupplyList = new SupplyListContract
                {
                    Items = supplies
                        .Select(x => x.ToListItemContract())
                        .ToArray(),
                    RowsCount = rowsCount
                }
            };
        }
    }
}