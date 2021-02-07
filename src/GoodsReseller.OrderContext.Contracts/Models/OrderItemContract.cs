using System;

namespace GoodsReseller.OrderContext.Contracts.Models
{
    public class OrderItemContract
    {
        public Guid Id { get; set; }
        
        public Guid ProductId { get; set; }
        
        public MoneyContract UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountPerUnit { get; set; }
    }
}