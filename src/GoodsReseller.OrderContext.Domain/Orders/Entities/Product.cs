using System;
using GoodsReseller.SeedWork;

namespace GoodsReseller.OrderContext.Domain.Orders.Entities
{
    public sealed class Product : VersionedEntity
    {
        public string Name { get; }
        public string Label { get; }

        public Product(Guid id, int version, string name, string label) : base(id, version)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (label == null)
            {
                throw new ArgumentNullException(nameof(label));
            }
            
            Name = name;
            Label = label;
        }
    }
}