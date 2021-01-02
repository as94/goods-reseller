using System;
using System.Collections.Generic;
using GoodsReseller.Domain.SeedWork;

namespace GoodsReseller.Domain.Orders.ValueObjects
{
    public sealed class Money : ValueObject
    {
        public decimal Value { get; }
        public Currency Currency { get; }

        public Money(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Value shouldn't be negative");
            }
            
            Value = value;
            Currency = Currency.RUB;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Currency;
        }
    }
}