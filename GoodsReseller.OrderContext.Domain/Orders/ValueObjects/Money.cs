using System;
using System.Collections.Generic;
using GoodsReseller.SeedWork;

namespace GoodsReseller.OrderContext.Domain.Orders.ValueObjects
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
        
        public static Money Zero = new Money(0);

        public Money Add(Money money)
        {
            return new Money(Value + money.Value);
        }
        
        public Money Subtract(Money money)
        {
            return new Money(Value - money.Value);
        }

        public Money Multiply(Factor factor)
        {
            return new Money(Value * factor.Value);
        }
    }
}