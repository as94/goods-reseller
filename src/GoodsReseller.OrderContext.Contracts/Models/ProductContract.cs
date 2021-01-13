using System;

namespace GoodsReseller.OrderContext.Contracts.Models
{
    public class ProductContract
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
    }
}