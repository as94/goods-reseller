using System;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;
using GoodsReseller.SeedWork;
using GoodsReseller.SeedWork.ValueObjects;

namespace GoodsReseller.OrderContext.Domain.Orders.Entities
{
    public sealed class OrderItem : Entity
    {
        public Guid ProductId { get; }
        public Money UnitPrice { get; }
        public Quantity Quantity { get; private set; }
        public Discount DiscountPerUnit { get; }

        public OrderItem(Guid id, Guid productId, Money unitPrice, Quantity quantity, Discount discountPerUnit)
            : base(id)
        {
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
            
            ProductId = productId;
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