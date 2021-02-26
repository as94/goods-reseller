using System;
using GoodsReseller.SeedWork;
using GoodsReseller.SeedWork.ValueObjects;

namespace GoodsReseller.SupplyContext.Domain.Supplies.Entities
{
    public sealed class SupplyItem : Entity
    {
        public Guid ProductId { get; private set; }
        public Money UnitPrice { get; private set; }
        public Quantity Quantity { get; private set; }
        public Discount DiscountPerUnit { get; private set; }
        
        public SupplyItem(Guid id, Guid productId, Money unitPrice, Quantity quantity, Discount discountPerUnit)
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

        private SupplyItem(Guid id, Guid productId) : base(id)
        {
            ProductId = productId;
        }

        public void Update(SupplyItem supplyItem)
        {
            if (supplyItem == null)
            {
                throw new ArgumentNullException(nameof(supplyItem));
            }

            ProductId = supplyItem.ProductId;
            UnitPrice = supplyItem.UnitPrice;
            Quantity = supplyItem.Quantity;
            DiscountPerUnit = supplyItem.DiscountPerUnit;
        }
    }
}