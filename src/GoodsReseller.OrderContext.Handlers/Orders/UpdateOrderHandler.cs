using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.OrderContext.Contracts.Orders.Update;
using GoodsReseller.OrderContext.Domain.Orders;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
using GoodsReseller.OrderContext.Handlers.Converters;
using MediatR;

namespace GoodsReseller.OrderContext.Handlers.Orders
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderRequest, Unit>
    {
        private readonly IOrdersRepository _ordersRepository;

        public UpdateOrderHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<Unit> Handle(UpdateOrderRequest request, CancellationToken cancellationToken)
        {
            var order = await _ordersRepository.GetAsync(request.OrderId, cancellationToken);

            if (order == null)
            {
                throw new InvalidOperationException($"Order with Id = {request.OrderId} doesn't exist");
            }

            var orderInfo = new OrderInfo(
                request.OrderInfo.Status,
                request.OrderInfo.Address.ToDomain(),
                request.OrderInfo.CustomerInfo.ToDomain(),
                request.OrderInfo.DeliveryCost.ToDomain(),
                request.OrderInfo.OrderItems.Select(x => x.ToDomain()));
            
            order.Update(orderInfo);

            await _ordersRepository.SaveAsync(order, cancellationToken);
            
            return new Unit();
        }
    }
}