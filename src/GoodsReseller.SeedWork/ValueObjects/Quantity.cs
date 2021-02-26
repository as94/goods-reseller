using System;
using System.Collections.Generic;

namespace GoodsReseller.SeedWork.ValueObjects
{
    public sealed class Quantity : ValueObject
    {
        public int Value { get; }

        public Quantity(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Value shouldn't be negative");
            }
            
            // TODO: business rule, add translations
            if (value > 100)
            {
                throw new ArgumentException("Value shouldn't be more than 100");
            }
            
            Value = value;
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}