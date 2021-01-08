using System;
using System.Collections.Generic;

namespace GoodsReseller.SeedWork.ValueObjects
{
    public sealed class Factor : ValueObject
    {
        public Factor(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Value shouldn't be negative");
            }
            
            Value = value;
        }

        public decimal Value { get; }
        
        public static Factor Empty => new Factor(0);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}