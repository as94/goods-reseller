using GoodsReseller.OrderContext.Contracts.Models;
using GoodsReseller.OrderContext.Domain.Orders.Entities;

namespace GoodsReseller.OrderContext.Handlers.Converters
{
    internal static class OrderItemConverters
    {
        public static OrderItemContract ToContract(this OrderItem orderItem)
        {
            return new OrderItemContract
            {
                Id = orderItem.Id,
                ProductId = orderItem.ProductId,
                UnitPrice = orderItem.UnitPrice.ToContract(),
                Quantity = orderItem.Quantity.Value,
                DiscountPerUnit = orderItem.DiscountPerUnit.Value
            };
        }
    }
}