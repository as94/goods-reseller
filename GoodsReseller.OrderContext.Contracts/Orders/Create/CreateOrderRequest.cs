using MediatR;

namespace GoodsReseller.OrderContext.Contracts.Orders.Create
{
    public class CreateOrderRequest : IRequest<CreateOrderResponse>
    {
    }
}