using System;

namespace GoodsReseller.OrderContext.Handlers.OrderItems.Commands
{
    public sealed class OrderItemCommandParameters
    {
        public OrderItemCommandParameters(Guid orderId, Guid productId)
        {
            OrderId = orderId;
            ProductId = productId;
        }

        public Guid OrderId { get; }
        public Guid ProductId { get; }
    }
}