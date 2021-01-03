using System;
using GoodsReseller.Domain.SeedWork;

namespace GoodsReseller.Domain.Orders.Entities
{
    public sealed class Product : Entity
    {
        public string Name { get; }
        
        public Product(Guid id, int version, string name) : base(id, version)
        {
            Name = name;
        }
    }
}