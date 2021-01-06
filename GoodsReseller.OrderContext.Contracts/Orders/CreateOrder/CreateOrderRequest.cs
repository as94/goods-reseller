using MediatR;

namespace GoodsReseller.OrderContext.Contracts.Orders.CreateOrder
{
    public class CreateOrderRequest : IRequest<CreateOrderResponse>
    {
    }
}