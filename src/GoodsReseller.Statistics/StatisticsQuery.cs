using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoodsReseller.Statistics
{
    public class StatisticsQuery : IValidatableObject
    {
        [Required]
        public int Year { get; set; }

        public int? Month { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Year < 2000)
            {
                yield return new ValidationResult("Year should be more than 2000");
            }

            if (Month.HasValue && (Month < 1 || Month > 12))
            {
                yield return new ValidationResult("Month should be between 1 and 12");
            }
        }
    }
}