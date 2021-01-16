using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GoodsReseller.AuthContext.Domain.ValidationRules;

namespace GoodsReseller.AuthContext.Contracts.Models
{
    public class LoginUserContract : IValidatableObject
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!EmailValidator.IsValid(Email, out _))
            {
                yield return new ValidationResult($"Email '{Email}' is invalid ");
            }
        }
    }
}