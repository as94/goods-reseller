using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.OrderContext.Contracts.Models;
using GoodsReseller.OrderContext.Contracts.Orders.BatchByQuery;
using GoodsReseller.OrderContext.Domain.Orders;
using GoodsReseller.OrderContext.Handlers.Converters;
using MediatR;

namespace GoodsReseller.OrderContext.Handlers.Orders
{
    public class BatchOrdersByQueryHandler : IRequestHandler<BatchOrdersByQueryRequest, BatchOrdersByQueryResponse>
    {
        private readonly IOrdersRepository _ordersRepository;

        public BatchOrdersByQueryHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }
        
        public async Task<BatchOrdersByQueryResponse> Handle(BatchOrdersByQueryRequest request, CancellationToken cancellationToken)
        {
            var (orders, rowsCount) = await _ordersRepository.BatchAsync(request.Query.Offset, request.Query.Count, cancellationToken);

            return new BatchOrdersByQueryResponse
            {
                OrderList = new OrderListContract
                {
                    Items = orders
                        .Select(x => x.ToListItemContract())
                        .ToArray(),
                    RowsCount = rowsCount
                }
            };
        }
    }
}