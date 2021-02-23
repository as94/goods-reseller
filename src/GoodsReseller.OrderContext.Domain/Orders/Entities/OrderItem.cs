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
        
        public DateValueObject CreationDate { get; }
        public DateValueObject? LastUpdateDate { get; private set; }
        public bool IsRemoved { get; private set; }

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
            CreationDate = new DateValueObject();
            IsRemoved = false;
        }

        private OrderItem(Guid id, Guid productId) : base(id)
        {
            ProductId = productId;
        }

        public void IncrementQuantity(DateValueObject lastUpdateDate)
        {
            Quantity = new Quantity(Quantity.Value + 1);
            LastUpdateDate = lastUpdateDate;
        }

        public void DecrementQuantity(DateValueObject lastUpdateDate)
        {
            Quantity = new Quantity(Quantity.Value - 1);
            LastUpdateDate = lastUpdateDate;
        }
        
        public void Remove(DateValueObject lastUpdateDate)
        {
            if (IsRemoved)
            {
                return;
            }
            
            if (lastUpdateDate == null)
            {
                throw new ArgumentNullException(nameof(lastUpdateDate));
            }
            
            IsRemoved = true;
            LastUpdateDate = lastUpdateDate;
        }
    }
}