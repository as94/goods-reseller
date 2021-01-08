using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GoodsReseller.OrderContext.Contracts.Models
{
    public class PatchOrderItemContract : IValidatableObject
    {
        public string Op { get; set; }
        public Guid ProductId { get; set; }
        
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!OrderItemOperations.AllOperations.Contains(Op))
            {
                yield return new ValidationResult(
                    $"Available operations are '{string.Join(",", OrderItemOperations.AllOperations)}'");
            }
        }
    }
}