using GoodsReseller.OrderContext.Contracts.Models;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
using GoodsReseller.SeedWork.ValueObjects;

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
                UnitPrice = orderItem.UnitPrice.Value,
                Quantity = orderItem.Quantity.Value,
                DiscountPerUnit = orderItem.DiscountPerUnit.Value
            };
        }

        public static OrderItem ToDomain(this OrderItemContract orderItem)
        {
            return new OrderItem(
                orderItem.Id,
                orderItem.ProductId,
                new Money(orderItem.UnitPrice),
                new Quantity(orderItem.Quantity),
                new Discount(orderItem.DiscountPerUnit));
        }
    }
}