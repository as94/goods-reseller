using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.OrderContext.Contracts.Orders.Update;
using MediatR;

namespace GoodsReseller.OrderContext.Handlers.Orders
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderRequest, UpdateOrderResponse>
    {
        public Task<UpdateOrderResponse> Handle(UpdateOrderRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}