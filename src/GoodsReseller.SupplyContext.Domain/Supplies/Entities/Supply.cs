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
        public SupplierInfo SupplierInfo { get; private set; }
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

        public void Update(SupplyInfo supplyInfo)
        {
            if (IsRemoved)
            {
                throw new InvalidOperationException($"Supply with id = {Id} has already been removed");
            }
            
            if (supplyInfo == null)
            {
                throw new ArgumentNullException(nameof(supplyInfo));
            }

            SupplierInfo = supplyInfo.SupplierInfo.Copy();

            var existingSupplyItemIds = SupplyItems.Select(x => x.Id).ToArray();
            var incomingSupplyItemIds = supplyInfo.SupplyItems.Select(x => x.Id).ToArray();

            var toCreateIds = incomingSupplyItemIds.Where(id => !existingSupplyItemIds.Contains(id));
            var newSupplyItems = supplyInfo.SupplyItems.Where(x => toCreateIds.Contains(x.Id));
            foreach (var newSupplyItem in newSupplyItems)
            {
                _supplyItems.Add(newSupplyItem);
            }

            var toUpdateIds = existingSupplyItemIds.Where(id => incomingSupplyItemIds.Contains(id));
            foreach (var id in toUpdateIds)
            {
                var existing = _supplyItems.First(x => x.Id == id);
                var incoming = supplyInfo.SupplyItems.First(x => x.Id == id);
                existing.Update(incoming);
            }

            var toDeleteIds = existingSupplyItemIds.Where(id => !incomingSupplyItemIds.Contains(id));
            foreach (var id in toDeleteIds)
            {
                var existing = _supplyItems.First(x => x.Id == id);
                existing.Remove();
            }
            
            RecalculateTotalCost();
            
            LastUpdateDate = new DateValueObject();
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