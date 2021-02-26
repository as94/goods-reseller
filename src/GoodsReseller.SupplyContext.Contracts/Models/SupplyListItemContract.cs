using System;

namespace GoodsReseller.SupplyContext.Contracts.Models
{
    public class SupplyListItemContract
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string SupplierName { get; set; }
        public decimal TotalCost { get; set; }
    }
}