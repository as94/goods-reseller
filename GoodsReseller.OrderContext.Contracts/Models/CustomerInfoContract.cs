using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GoodsReseller.OrderContext.Domain.Orders.ValidationRules;

namespace GoodsReseller.OrderContext.Contracts.Models
{
    public class CustomerInfoContract : IValidatableObject
    {
        [Required]
        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!PhoneNumberValidator.IsValid(PhoneNumber))
            {
                yield return new ValidationResult($"Phone number '{PhoneNumber}' is invalid");
            }
        }
    }
}