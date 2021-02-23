using System;
using System.Collections.Generic;

namespace GoodsReseller.SeedWork.ValueObjects
{
    public sealed class DateValueObject : ValueObject, IComparable<DateValueObject>
    {
        public DateValueObject()
        {
            Date = DateTime.Now;
            DateUtc = Date.ToUniversalTime();
        }

        /// <summary>
        /// EF constructor
        /// </summary>
        private DateValueObject(DateTime date, DateTime dateUtc)
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

        public int CompareTo(DateValueObject other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var dateUtcComparison = DateUtc.CompareTo(other.DateUtc);
            if (dateUtcComparison != 0) return dateUtcComparison;
            return Date.CompareTo(other.Date);
        }
    }
}