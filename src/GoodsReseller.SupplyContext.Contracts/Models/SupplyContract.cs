using System;

namespace GoodsReseller.SupplyContext.Contracts.Models
{
    public class SupplyContract
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public SupplierInfoContract SupplierInfo { get; set; }
        public SupplyItemContract[] SupplyItems { get; set; } = Array.Empty<SupplyItemContract>();
        public MoneyContract TotalCost { get; set; }
    }
}