using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.OrderContext.Contracts.Orders.Create;
using GoodsReseller.OrderContext.Domain.Orders;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;
using GoodsReseller.OrderContext.Handlers.Converters;
using GoodsReseller.SeedWork.ValueObjects;
using MediatR;

namespace GoodsReseller.OrderContext.Handlers.Orders
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderRequest, Unit>
    {
        private readonly IOrdersRepository _ordersRepository;

        public CreateOrderHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }
        
        public async Task<Unit> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var order = new Order(
                request.OrderInfo.Id,
                request.OrderInfo.Version,
                OrderStatus.Accepted,
                request.OrderInfo.Address.ToDomain(),
                request.OrderInfo.CustomerInfo.ToDomain(),
                new Money(request.OrderInfo.DeliveryCost),
                new Money(request.OrderInfo.AddedCost),
                request.OrderInfo.OrderItems.Select(x => x.ToDomain()));
            
            await _ordersRepository.SaveAsync(order, cancellationToken);

            return Unit.Value;
        }
    }
}