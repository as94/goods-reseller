using System;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;

namespace GoodsReseller.Infrastructure.Orders.Models
{
    internal sealed class OrderItemState
    {
        public Guid Id { get; set; }
        
        public Product Product { get; set; }
        public Money UnitPrice { get; set; }
        public Quantity Quantity { get; set; }
        public Factor DiscountPerUnit { get; set; }

        public OrderItem ToDomain()
        {
            return new OrderItem(Id, Product, UnitPrice, Quantity, DiscountPerUnit);
        }
    }
}