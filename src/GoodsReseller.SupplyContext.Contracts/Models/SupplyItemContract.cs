using System;

namespace GoodsReseller.SupplyContext.Contracts.Models
{
    public class SupplyItemContract
    {
        public Guid Id { get; set; }
        
        public Guid ProductId { get; set; }
        
        public MoneyContract UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountPerUnit { get; set; }
    }
}