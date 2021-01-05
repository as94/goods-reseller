using System;

namespace GoodsReseller.OrderContext.Contracts.Models
{
    public class OrderContract
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public OrderItemContract[] OrderItems { get; set; } = Array.Empty<OrderItemContract>();
        public MoneyContract TotalCost { get; set; }
    }
}