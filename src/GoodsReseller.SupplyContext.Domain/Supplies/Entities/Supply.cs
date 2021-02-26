using System;
using System.Collections.Generic;
using System.Linq;
using GoodsReseller.SeedWork;
using GoodsReseller.SeedWork.ValueObjects;
using GoodsReseller.SupplyContext.Domain.Supplies.ValueObjects;

namespace GoodsReseller.SupplyContext.Domain.Supplies.Entities
{
    public sealed class Supply : Entity, IAggregateRoot
    {
        private readonly List<SupplyItem> _supplyItems;
        public IReadOnlyCollection<SupplyItem> SupplyItems => _supplyItems.Where(x => !x.IsRemoved).ToList();
        public SupplierInfo SupplierInfo { get; }
        public Money TotalCost { get; private set; }
        

        public Supply(Guid id, SupplierInfo supplierInfo, IEnumerable<SupplyItem> supplyItems)
            : this(id)
        {
            if (supplierInfo == null)
            {
                throw new ArgumentNullException(nameof(supplierInfo));
            }

            SupplierInfo = supplierInfo;
            _supplyItems = new List<SupplyItem>(supplyItems);
            RecalculateTotalCost();
        }
        
        private Supply(Guid id) : base(id)
        {
            _supplyItems = new List<SupplyItem>();
        }
        
        private void RecalculateTotalCost()
        {
            var totalCost = Money.Zero;

            foreach (var orderItem in SupplyItems)
            {
                var unitPriceFactor = new Factor(1 - orderItem.DiscountPerUnit.Value);
                var quantityFactor = new Factor(orderItem.Quantity.Value);

                var orderItemValue = orderItem.UnitPrice.Multiply(unitPriceFactor).Multiply(quantityFactor);
                
                totalCost = totalCost.Add(orderItemValue);
            }

            TotalCost = totalCost;
        }
    }
}