using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoodsReseller.DataCatalogContext.Contracts.Queries
{
    public class BatchProductsQuery : IValidatableObject
    {
        public int Offset { get; set; } = 0;

        public int Count { get; set; } = 10;
        
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

            if (Count > 50)
            {
                yield return new ValidationResult("Count should be less or equal 50");
            }
        }
    }
}