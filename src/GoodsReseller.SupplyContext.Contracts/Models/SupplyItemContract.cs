using System;
using System.ComponentModel.DataAnnotations;

namespace GoodsReseller.SupplyContext.Contracts.Models
{
    public class SupplyItemContract
    {
        public Guid Id { get; set; }
        
        public Guid ProductId { get; set; }
        
        [Required]
        public MoneyContract UnitPrice { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public decimal DiscountPerUnit { get; set; }
    }
}