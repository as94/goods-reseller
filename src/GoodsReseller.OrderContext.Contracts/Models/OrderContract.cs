using System;

namespace GoodsReseller.OrderContext.Contracts.Models
{
    public class OrderContract
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public AddressContract Address { get; set; }
        public CustomerInfoContract CustomerInfo { get; set; }
        public OrderItemContract[] OrderItems { get; set; } = Array.Empty<OrderItemContract>();
        public decimal DeliveryCost { get; set; }
        public decimal AddedCost { get; set; }
        public decimal TotalCost { get; set; }
    }
}