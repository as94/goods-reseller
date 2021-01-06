using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoodsReseller.OrderContext.Contracts.Models
{
    public class OrderItemContract : IValidatableObject
    {
        public Guid Id { get; set; }
        
        [Required]
        public ProductContract Product { get; set; }
        
        [Required]
        public MoneyContract UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountPerUnit { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Quantity <= 0)
            {
                yield return new ValidationResult("Quantity should be more than 0");
            }

            if (DiscountPerUnit < 0)
            {
                yield return new ValidationResult("Discount per unit should be positive");
            }
        }
    }
}