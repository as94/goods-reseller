using System;
using System.Collections.Generic;

namespace GoodsReseller.SeedWork.ValueObjects
{
    public sealed class DateValueObject : ValueObject
    {
        public DateValueObject(DateTime date)
        {
            Date = date;
            DateUtc = date.ToUniversalTime();
        }

        public DateTime Date { get; }
        public DateTime DateUtc { get; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Date;
            yield return DateUtc;
        }
    }
}