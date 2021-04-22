using System;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;
using GoodsReseller.SeedWork;
using GoodsReseller.SeedWork.ValueObjects;

namespace GoodsReseller.OrderContext.Domain.Orders.Entities
{
    public sealed class OrderItem : Entity
    {
        public Guid ProductId { get; private set; }
        public Money UnitPrice { get; private set; }
        public Quantity Quantity { get; private set; }
        public Discount DiscountPerUnit { get; private set; }

        public OrderItem(Guid id, Guid productId, Money unitPrice, Quantity quantity, Discount discountPerUnit)
            : this(id, productId)
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

            UnitPrice = unitPrice;
            Quantity = quantity;
            DiscountPerUnit = discountPerUnit;
        }

        private OrderItem(Guid id, Guid productId) : base(id)
        {
            ProductId = productId;
        }
        
        public void Update(OrderItem orderItem)
        {
            if (orderItem == null)
            {
                throw new ArgumentNullException(nameof(orderItem));
            }

            ProductId = orderItem.ProductId;
            UnitPrice = orderItem.UnitPrice;
            Quantity = orderItem.Quantity;
            DiscountPerUnit = orderItem.DiscountPerUnit;
        }
    }
}