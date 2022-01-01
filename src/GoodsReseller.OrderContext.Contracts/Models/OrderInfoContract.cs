using System;
using System.ComponentModel.DataAnnotations;

namespace GoodsReseller.OrderContext.Contracts.Models
{
    public class OrderInfoContract
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public int Version { get; set; }
        
        [Required]
        public string Status { get; set; }
        
        [Required]
        public AddressContract Address { get; set; }
        
        [Required]
        public CustomerInfoContract CustomerInfo { get; set; }
        
        [Required]
        public MoneyContract DeliveryCost { get; set; }
        
        [Required]
        public MoneyContract AddedCost { get; set; }
        
        [Required]
        public OrderItemContract[] OrderItems { get; set; } = Array.Empty<OrderItemContract>();
    } 
}