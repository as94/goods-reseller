using System;
using System.Collections.Generic;

namespace GoodsReseller.Domain.SeedWork
{
    public sealed class DateAndDateUtcPair : ValueObject
    {
        public DateAndDateUtcPair(DateTime date, DateTime dateUtc)
        {
            Date = date;
            DateUtc = dateUtc;
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