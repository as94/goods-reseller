using System;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;
using GoodsReseller.SeedWork;
using GoodsReseller.SeedWork.ValueObjects;

namespace GoodsReseller.OrderContext.Domain.Orders.Entities
{
    public sealed class OrderItem : Entity
    {
        public Product Product { get; }
        public Money UnitPrice { get; }
        public Quantity Quantity { get; private set; }
        public Factor DiscountPerUnit { get; }

        public OrderItem(Guid id, Product product, Money unitPrice, Quantity quantity, Factor discountPerUnit)
            : base(id)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            
            if (unitPrice == null)
            {
                throw new ArgumentNullException(nameof(unitPrice));
            }
            
            if (discountPerUnit == null)
            {
                throw new ArgumentNullException(nameof(discountPerUnit));
            }
            
            if (quantity == null)
            {
                throw new ArgumentNullException(nameof(quantity));
            }
            
            Product = product;
            UnitPrice = unitPrice;
            Quantity = quantity;
            DiscountPerUnit = discountPerUnit;
        }

        public void IncrementQuantity()
        {
            Quantity = new Quantity(Quantity.Value + 1);
        }

        public void DecrementQuantity()
        {
            Quantity = new Quantity(Quantity.Value - 1);
        }
    }
}