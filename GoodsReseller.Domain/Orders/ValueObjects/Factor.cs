using System;
using System.Collections.Generic;
using GoodsReseller.Domain.SeedWork;

namespace GoodsReseller.Domain.Orders.ValueObjects
{
    public sealed class Factor : ValueObject
    {
        public Factor(double value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Value shouldn't be negative");
            }

            if (value > 1)
            {
                
                throw new ArgumentException("Value shouldn't be more than 1");
            }
            
            Value = value;
        }

        public double Value { get; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}