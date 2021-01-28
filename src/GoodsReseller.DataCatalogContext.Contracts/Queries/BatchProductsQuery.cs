using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoodsReseller.DataCatalogContext.Contracts.Queries
{
    public class BatchProductsQuery : IValidatableObject
    {
        public const int MaxCount = 1000;
        
        public int Offset { get; set; } = 0;

        public int Count { get; set; } = MaxCount;
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Offset < 0)
            {
                yield return new ValidationResult("Offset should be positive");
            }
            
            if (Count < 0)
            {
                yield return new ValidationResult("Count should be positive");
            }

            if (Count > MaxCount)
            {
                yield return new ValidationResult($"Count should be less or equal {MaxCount}");
            }
        }
    }
}