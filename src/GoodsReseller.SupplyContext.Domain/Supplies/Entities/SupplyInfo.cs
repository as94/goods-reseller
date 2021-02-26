using System;
using System.Collections.Generic;
using GoodsReseller.SupplyContext.Domain.Supplies.ValueObjects;

namespace GoodsReseller.SupplyContext.Domain.Supplies.Entities
{
    public class SupplyInfo
    {
        public SupplyInfo(SupplierInfo supplierInfo, IEnumerable<SupplyItem> supplyItems)
        {
            if (supplierInfo == null)
            {
                throw new ArgumentNullException(nameof(supplierInfo));
            }

            if (supplyItems == null)
            {
                throw new ArgumentNullException(nameof(supplyItems));
            }
            
            SupplierInfo = supplierInfo;
            SupplyItems = supplyItems;
        }

        public SupplierInfo SupplierInfo { get; }
        public IEnumerable<SupplyItem> SupplyItems { get; }
    }
}