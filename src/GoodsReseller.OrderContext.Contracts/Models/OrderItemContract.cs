using System;
using System.ComponentModel.DataAnnotations;

namespace GoodsReseller.OrderContext.Contracts.Models
{
    public class OrderItemContract
    {
        public Guid Id { get; set; }
        
        public Guid ProductId { get; set; }
        
        [Required]
        public decimal UnitPrice { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public decimal DiscountPerUnit { get; set; }
    }
}