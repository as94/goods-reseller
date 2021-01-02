using System;
using GoodsReseller.Domain.SeedWork;

namespace GoodsReseller.Domain.Orders.Entities
{
    public sealed class Product : Entity
    {
        public string Name { get; }
        
        public Product(Guid id, string name) : base(id)
        {
            Name = name;
        }
    }
}