using System;
using System.Collections.Generic;
using GoodsReseller.SeedWork;

namespace GoodsReseller.SupplyContext.Domain.Supplies.ValueObjects
{
    public sealed class SupplierInfo : ValueObject
    {
        public string Name { get; }
        
        public SupplierInfo(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            Name = name;
        }
        
        public SupplierInfo Copy()
        {
            return new SupplierInfo(Name);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}