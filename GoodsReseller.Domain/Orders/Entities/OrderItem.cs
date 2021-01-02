using System;
using GoodsReseller.Domain.Orders.ValueObjects;
using GoodsReseller.Domain.SeedWork;

namespace GoodsReseller.Domain.Orders.Entities
{
    public sealed class OrderItem : Entity
    {
        public Product Product { get; }
        public Money UnitPrice { get; }
        public Factor TotalDiscount { get; }
        public Quantity Quantity { get; }

        public OrderItem(Guid id, Product product, Money unitPrice, Factor totalDiscount, Quantity quantity) : base(id)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            
            if (unitPrice == null)
            {
                throw new ArgumentNullException(nameof(unitPrice));
            }
            
            if (totalDiscount == null)
            {
                throw new ArgumentNullException(nameof(totalDiscount));
            }
            
            if (quantity == null)
            {
                throw new ArgumentNullException(nameof(quantity));
            }
            
            Product = product;
            UnitPrice = unitPrice;
            TotalDiscount = totalDiscount;
            Quantity = quantity;
        }
    }
}