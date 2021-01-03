using System;
using System.Collections.Generic;
using GoodsReseller.Domain.SeedWork;

namespace GoodsReseller.Domain.Orders.ValueObjects
{
    public sealed class Quantity : ValueObject
    {
        public int Value { get; private set; }

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