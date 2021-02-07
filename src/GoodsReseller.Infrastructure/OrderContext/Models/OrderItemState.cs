using System;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;
using GoodsReseller.SeedWork.ValueObjects;

namespace GoodsReseller.Infrastructure.OrderContext.Models
{
    internal sealed class OrderItemState
    {
        public Guid Id { get; set; }
        
        public Guid ProductId { get; set; }
        public Money UnitPrice { get; set; }
        public Quantity Quantity { get; set; }
        public Discount DiscountPerUnit { get; set; }

        public OrderItem ToDomain()
        {
            return new OrderItem(Id, ProductId, UnitPrice, Quantity, DiscountPerUnit);
        }
    }
}