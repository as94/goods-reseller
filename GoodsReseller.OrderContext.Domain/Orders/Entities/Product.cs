using System;
using GoodsReseller.Orders.Domain.SeedWork;

namespace GoodsReseller.OrderContext.Domain.Orders.Entities
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