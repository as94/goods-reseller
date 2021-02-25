using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.OrderContext.Contracts.Orders.Create;
using GoodsReseller.OrderContext.Domain.Orders;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;
using GoodsReseller.OrderContext.Handlers.Converters;
using MediatR;

namespace GoodsReseller.OrderContext.Handlers.Orders
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderRequest, CreateOrderResponse>
    {
        private readonly IOrdersRepository _ordersRepository;

        public CreateOrderHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }
        
        public async Task<CreateOrderResponse> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var orderId = Guid.NewGuid();
            var version = 1;

            var address = request.Address.ToDomain();
            var customerInfo = request.CustomerInfo.ToDomain();
            
            var order = new Order(orderId, version, OrderStatus.DataReceived.Name, address, customerInfo);
            await _ordersRepository.SaveAsync(order, cancellationToken);
            
            return new CreateOrderResponse
            {
                OrderId = orderId
            };
        }
    }
}