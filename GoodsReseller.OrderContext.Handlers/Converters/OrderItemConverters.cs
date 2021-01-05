using System;
using GoodsReseller.OrderContext.Contracts.Models;
using GoodsReseller.OrderContext.Domain.Orders.Entities;

namespace GoodsReseller.OrderContext.Handlers.Converters
{
    public static class OrderItemConverters
    {
        public static OrderItemContract ToContract(this OrderItem orderItem)
        {
            if (orderItem == null)
            {
                throw new ArgumentNullException(nameof(orderItem));
            }
            
            return new OrderItemContract
            {
                Id = orderItem.Id,
                Version = orderItem.Version,
                Product = orderItem.Product.ToContract(),
                UnitPrice = orderItem.UnitPrice.ToContract(),
                Quantity = orderItem.Quantity.Value,
                DiscountPerUnit = orderItem.DiscountPerUnit.Value
            };
        }
    }
}