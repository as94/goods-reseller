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
        public Quantity Quantity { get; private set; }

        public OrderItem(Guid id, int version, Product product, Money unitPrice, Factor totalDiscount, Quantity quantity)
            : base(id, version)
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

        public void IncrementQuantity()
        {
            Quantity = new Quantity(Quantity.Value + 1);
            IncrementVersion();
        }

        public void DecrementQuantity()
        {
            Quantity = new Quantity(Quantity.Value - 1);
            IncrementVersion();
        }
    }
}