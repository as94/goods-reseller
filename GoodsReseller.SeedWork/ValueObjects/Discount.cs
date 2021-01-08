using System;
using System.Collections.Generic;

namespace GoodsReseller.SeedWork.ValueObjects
{
    public sealed class Discount : ValueObject
    {
        public decimal Value { get; }

        public Discount(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Factor shouldn't be negative");
            }
            
            if (value > 1)
            {
                throw new ArgumentException("Discount shouldn't be more than 1");
            }

            Value = value;
        }
        
        public static Discount Empty => new Discount(0);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}